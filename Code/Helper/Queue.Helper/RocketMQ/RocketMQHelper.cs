using NewLife.RocketMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Helper.RocketMQ
{
    /// <summary>
    /// RocketMQ 消息队列帮助类
    /// 创建日期:2023年10月24日
    /// </summary>
    public class RocketMQHelper
    {
        /// <summary>
        /// 生产者
        /// </summary>
        private Producer producer;

        /// <summary>
        /// 消费者
        /// </summary>
        private Consumer consumer;

        /// <summary>
        /// 消息回调
        /// </summary>
        public event Action<string> MessageCallback;

        /// <summary>
        /// 注册生产者
        /// </summary>
        /// <param name="nameServer">服务地址</param>
        /// <param name="topic">主题 需要提前创建</param>
        public void RegisterProducer(string nameServer, string topic)
        {
            producer = new Producer()
            {
                NameServerAddress = nameServer,
                Topic = topic,
            };

            producer.Start();
        }

        /// <summary>
        /// 注册消费者
        /// </summary>
        /// <param name="nameServer">服务地址</param>
        /// <param name="topic">主题 需要提前创建</param>
        /// <param name="group">消费组</param>
        /// <param name="batchSize">拉取的批大小</param>
        /// <param name="isNotice">通知消息队是否消费了消息</param>
        public void RegisterConsumer(string nameServer, string topic, string group, int batchSize = 1, bool isNotice = true)
        {
            consumer = new Consumer
            {
                NameServerAddress = nameServer,
                Topic = topic,
                Group = group,
                BatchSize = batchSize,
                OnConsume = (q, ms) =>
                {
                    //string mInfo = $"BrokerName={q.BrokerName},QueueId={q.QueueId},Length={ms.Length}";

                    foreach (var item in ms.ToList())
                    {
                        //string msg = string.Format($"接收到消息：msgId={item.MsgId},key={item.Keys}，产生时间【{item.BornTimestamp.ToDateTime()}】，内容：{item.BodyString}");

                        MessageCallback?.Invoke(item.BodyString);
                    }
                    return isNotice;
                }
            };

            consumer.Start();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        public void SendMessage(string message)
        {
            try
            {
                var sr = producer.Publish(message);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            producer?.Stop();
            consumer?.Stop();
        }
    }
}

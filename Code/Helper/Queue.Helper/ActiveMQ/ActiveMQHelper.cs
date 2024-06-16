using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.ActiveMQ;
using Apache.NMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Helper.ActiveMQ
{
    /// <summary>
    /// ActiveMQ 消息队列帮助类
    /// 创建日期:2023年10月26日
    /// </summary>
    public class ActiveMQHelper
    {
        IConnectionFactory _factory;

        // 消费者
        IConnection _connection_producer;
        ISession _session_producer;
        IMessageProducer _prod;

        // 生产者
        IConnection _connection_consumer;
        ISession _session_consumer;

        /// <summary>
        /// 消息回调
        /// </summary>
        public event Action<string> MessageCallback;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="brokerUri">服务地址</param>
        public ActiveMQHelper(string brokerUri)
        {
            // tcp://127.0.0.1:61616/
            _factory = new ConnectionFactory(brokerUri);
        }

        /// <summary>
        /// 注册生产者
        /// </summary>
        /// <param name="topic">主题</param>
        public void RegisterProducer(string topic)
        {
            _connection_producer = _factory.CreateConnection();
            _session_producer = _connection_producer.CreateSession();
            _prod = _session_producer.CreateProducer(new ActiveMQTopic(topic));
        }

        /// <summary>
        /// 注册消费者
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="clientid">客户端ID</param>
        public void RegisterConsumer(string topic, string clientid)
        {
            _connection_consumer = _factory.CreateConnection();
            _connection_consumer.ClientId = clientid;
            _connection_consumer.Start();
            //Create the Session
            _session_consumer = _connection_consumer.CreateSession();
            //Create the Consumer
            IMessageConsumer consumer = _session_consumer.CreateDurableConsumer(new ActiveMQTopic(topic), clientid, null, false);
            consumer.Listener += new MessageListener(consumer_Listener);
        }

        /// <summary>
        /// 消费者回调
        /// </summary>
        /// <param name="message"></param>
        private void consumer_Listener(IMessage message)
        {
            try
            {
                ITextMessage msg = (ITextMessage)message;
                MessageCallback?.Invoke(msg.Text);
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMessage(string message)
        {
            ITextMessage msg = _prod.CreateTextMessage();
            msg.Text = message;
            _prod.Send(msg, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            // 释放生产者
            _prod?.Close();
            _prod?.Dispose();
            _session_producer?.Close();
            _session_producer?.Dispose();
            _connection_producer?.Stop();
            _connection_producer?.Close();
            _connection_producer?.Dispose();

            // 释放消费者
            _session_consumer?.Close();
            _session_consumer?.Dispose();
            _connection_consumer?.Stop();
            _connection_consumer?.Close();
            _connection_consumer?.Dispose();
        }
    }
}

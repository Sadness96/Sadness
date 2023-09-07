using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Helper.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 消息队列帮助类
    /// 创建日期:2023年09月07日
    /// </summary>
    public class RabbitMQHelper
    {
        private IConnection _connection;
        private IModel _channel;
        private string _exchangeName;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostName">IP</param>
        /// <param name="port">端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public RabbitMQHelper(string hostName, int port = 5672, string userName = "", string password = "")
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                Port = port,
                UserName = userName,
                Password = password,
                // 自动重连
                AutomaticRecoveryEnabled = true,
                // 恢复拓扑结构
                TopologyRecoveryEnabled = true,
                // 后台处理消息
                UseBackgroundThreadsForIO = true,
                // 心跳超时时间
                RequestedHeartbeat = 60
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        /// <summary>
        /// 注册生产者
        /// </summary>
        /// <param name="exchangeName">交换机</param>
        /// <param name="queueName">队列</param>
        /// <param name="durable">持久化</param>
        /// <param name="ttl">生存时间</param>
        public void RegisterProducer(string exchangeName, string queueName, bool durable = true, TimeSpan? ttl = null)
        {
            _exchangeName = exchangeName;
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, durable);

            var arguments = new Dictionary<string, object>();
            if (ttl != null)
            {
                arguments.Add("x-message-ttl", (int)ttl.Value.TotalMilliseconds);
            }
            _channel.QueueDeclare(queueName, durable, false, false, arguments);
            _channel.QueueBind(queueName, exchangeName, routingKey: queueName);
        }

        /// <summary>
        /// 注册消费者
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="callback">回调函数</param>
        /// <param name="durable">持久化</param>
        /// <param name="ttl">生存时间</param>
        public void RegisterConsumer(string queueName, Action<string> callback, bool durable = true, TimeSpan? ttl = null)
        {
            var arguments = new Dictionary<string, object>();

            if (ttl != null)
            {
                arguments.Add("x-message-ttl", (int)ttl.Value.TotalMilliseconds);
            }

            _channel.QueueDeclare(queueName, durable, false, false, arguments);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                callback(message);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="routingKey">路由键</param>
        /// <param name="message">消息</param>
        /// <param name="persistent">消息持久化</param>
        public void SendMessage(string routingKey, string message, bool persistent = true)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = persistent;

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _exchangeName, routingKey: routingKey, basicProperties: properties, body: body);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}

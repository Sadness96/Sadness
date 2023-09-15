using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue.Helper.Socket
{
    /// <summary>
    /// Socket Tcp 服务端帮助类
    /// </summary>
    public class TcpServerHelper
    {
        private TcpListener listener;
        private List<TcpClient> clients;
        private Thread listenThread;

        /// <summary>
        /// 收到数据回调
        /// </summary>
        public event Action<EndPoint, string> OnDataReceived;

        /// <summary>
        ///  Socket Tcp 服务端构造函数
        /// </summary>
        /// <param name="port">服务端监听端口</param>
        public TcpServerHelper(int port)
        {
            Console.WriteLine($"Socket tcp Server listen port:{port}");

            listener = new TcpListener(IPAddress.Any, port);
            clients = new List<TcpClient>();
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            listenThread = new Thread(ListenForClients);
            listenThread.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            listenThread?.Abort();

            lock (clients)
            {
                foreach (var client in clients)
                {
                    client.Close();
                }

                clients.Clear();
            }
        }

        /// <summary>
        /// 监听客户端连接请求线程
        /// </summary>
        private void ListenForClients()
        {
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                lock (clients)
                {
                    Console.WriteLine($"Add client connection:{client.Client.RemoteEndPoint}");
                    clients.Add(client);
                }

                Thread clientThread = new Thread(HandleClientCommunication);
                clientThread.Start(client);
            }
        }

        /// <summary>
        /// 处理与客户端的通信线程
        /// </summary>
        /// <param name="clientObj"></param>
        private void HandleClientCommunication(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            byte[] buffer = new byte[1024];

            while (true)
            {
                try
                {
                    int bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        OnDataReceived?.Invoke(client.Client.RemoteEndPoint, receivedData);
                    }
                }
                catch
                {
                    break;
                }
            }

            lock (clients)
            {
                Console.WriteLine($"Break client connection:{client.Client.RemoteEndPoint}");
                clients.Remove(client);
            }

            client.Close();
        }

        /// <summary>
        /// 发送消息给所有客户端
        /// </summary>
        /// <param name="data">消息</param>
        public void SendDataToAll(string data)
        {
            lock (clients)
            {
                foreach (var client in clients)
                {
                    SendData(client, data);
                }
            }
        }

        /// <summary>
        /// 发送消息给指定客户端
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="data">消息</param>
        public void SendDataToClient(TcpClient client, string data)
        {
            if (!clients.Contains(client))
            {
                Console.WriteLine($"Client not connected to the server.");
                return;
            }

            SendData(client, data);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="client">客户端连接</param>
        /// <param name="data">数据</param>
        private void SendData(TcpClient client, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(buffer, 0, buffer.Length);
        }
    }
}

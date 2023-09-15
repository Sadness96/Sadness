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
    /// Socket UDP 服务端帮助类
    /// </summary>
    public class UdpServerHelper
    {
        private UdpClient udpServer;
        private List<IPEndPoint> clients;
        private Thread receiveThread;

        /// <summary>
        /// 收到数据回调
        /// </summary>
        public event Action<IPEndPoint, string> OnDataReceived;

        /// <summary>
        /// Socket UDP 服务端构造函数
        /// </summary>
        /// <param name="port">服务端监听端口</param>
        public UdpServerHelper(int port)
        {
            Console.WriteLine($"Socket UDP Server listen port:{port}");

            udpServer = new UdpClient(port);
            clients = new List<IPEndPoint>();
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            receiveThread = new Thread(ReceiveData);
            receiveThread.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            receiveThread?.Abort();
            udpServer?.Close();
        }

        /// <summary>
        /// 接收数据线程
        /// </summary>
        private void ReceiveData()
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    byte[] receivedBytes = udpServer.Receive(ref clientEndPoint);
                    string receivedData = Encoding.UTF8.GetString(receivedBytes);
                    OnDataReceived?.Invoke(clientEndPoint, receivedData);

                    if (!clients.Contains(clientEndPoint))
                    {
                        Console.WriteLine($"Add client connection:{clientEndPoint}");
                        clients.Add(clientEndPoint);
                    }

                }
                catch (SocketException)
                {
                    // SocketException will be thrown when the thread is aborted or the underlying socket is closed
                    Console.WriteLine($"Break client connection:{clientEndPoint}");
                    clients.Remove(clientEndPoint);
                }
            }
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
        public void SendDataToClient(IPEndPoint client, string data)
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
        private void SendData(IPEndPoint client, string data)
        {
            byte[] sendData = Encoding.UTF8.GetBytes(data);
            udpServer.Send(sendData, sendData.Length, client);
        }
    }
}

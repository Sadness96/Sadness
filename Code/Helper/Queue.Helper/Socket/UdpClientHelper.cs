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
    /// Socket UDP 客户端帮助类
    /// </summary>
    public class UdpClientHelper
    {
        private UdpClient udpClient;
        private IPEndPoint serverEndPoint;
        private Thread receiveThread;
        bool receiveThreadRunning = false;

        /// <summary>
        /// 收到数据回调
        /// </summary>
        public event Action<string> OnDataReceived;

        /// <summary>
        /// 连接到服务端
        /// </summary>
        /// <param name="ipAddress">IP</param>
        /// <param name="port">端口号</param>
        public void Connect(string ipAddress, int port)
        {
            serverEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            udpClient = new UdpClient();

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            udpClient.Client.Bind(localEndPoint);
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            receiveThreadRunning = true;
            receiveThread = new Thread(ReceiveData);
            receiveThread.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            receiveThreadRunning = false;
            receiveThread?.Join();
            udpClient?.Close();
            udpClient = null;
        }

        /// <summary>
        /// 接收数据线程
        /// </summary>
        private void ReceiveData()
        {
            IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    if (udpClient != null)
                    {
                        byte[] receivedData = udpClient.Receive(ref senderEndPoint);
                        string message = Encoding.UTF8.GetString(receivedData);
                        OnDataReceived?.Invoke(message);
                    }
                }
                catch (SocketException e)
                {
                    // SocketException will be thrown when the thread is aborted or the underlying socket is closed
                    Console.WriteLine($"UDP receive thread stopped: {e.Message}");
                }
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data">消息</param>
        public void SendData(string data)
        {
            if (udpClient != null)
            {
                byte[] sendData = Encoding.UTF8.GetBytes(data);
                udpClient.Send(sendData, sendData.Length, serverEndPoint);
            }
        }
    }
}

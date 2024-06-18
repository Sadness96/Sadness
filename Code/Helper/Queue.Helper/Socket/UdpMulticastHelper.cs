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
    /// Socket UDP 多播帮助类
    /// </summary>
    public class UdpMulticastHelper
    {
        private UdpClient udpClient;
        private IPAddress multicastAddress;
        private int port;
        private Thread receiveThread;
        private bool receiveThreadRunning = false;

        /// <summary>
        /// 收到数据回调
        /// </summary>
        public event Action<IPEndPoint, string> OnDataReceived;

        /// <summary>
        /// Socket UDP 多播构造函数
        /// </summary>
        /// <param name="multicastAddress">多播地址</param>
        /// <param name="port">多播端口</param>
        public UdpMulticastHelper(string multicastAddress, int port)
        {
            this.multicastAddress = IPAddress.Parse(multicastAddress);
            this.port = port;
            Console.WriteLine($"UDP Multicast Server listen on {this.multicastAddress}:{port}");

            udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            // Allow multiple clients on the same machine to use this port.
            udpClient.ExclusiveAddressUse = false;
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));

            udpClient.JoinMulticastGroup(this.multicastAddress);
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
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (receiveThreadRunning)
            {
                try
                {
                    if (udpClient != null)
                    {
                        byte[] receivedBytes = udpClient.Receive(ref clientEndPoint);
                        string receivedData = Encoding.UTF8.GetString(receivedBytes);
                        OnDataReceived?.Invoke(clientEndPoint, receivedData);
                    }
                }
                catch (SocketException)
                {
                    // SocketException will be thrown when the thread is aborted or the underlying socket is closed
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
                udpClient.Send(sendData, sendData.Length, new IPEndPoint(multicastAddress, port));
            }
        }
    }
}

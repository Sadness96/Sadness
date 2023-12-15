using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Helper.Socket
{
    /// <summary>
    /// Socket Tcp 客户端帮助类
    /// </summary>
    public class TcpClientHelper
    {
        private TcpClient client;
        private byte[] buffer;

        /// <summary>
        /// 收到数据回调
        /// </summary>
        public event Action<string> OnDataReceived;

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool IsConnected => client != null && client.Connected;

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ipAddress">IP</param>
        /// <param name="port">端口号</param>
        public void Connect(string ipAddress, int port)
        {
            buffer = new byte[1024];

            try
            {
                Console.WriteLine($"Socket tcp Client connection to {ipAddress}:{port}");

                client = new TcpClient();
                // 连接到服务端
                client.Connect(ipAddress, port);
                // 开始接收数据
                client.GetStream().BeginRead(buffer, 0, buffer.Length, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 断开连接释放资源
        /// </summary>
        public void Disconnect()
        {
            client?.Close();
            client?.Dispose();
            client = null;
        }

        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            if (!IsConnected)
            {
                Console.WriteLine($"Client is not connected to a server.");
                return;
            }

            try
            {
                int bytesRead = client.GetStream().EndRead(ar);

                if (bytesRead > 0)
                {
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    OnDataReceived?.Invoke(receivedData);
                }

                // 继续接收数据
                client.GetStream().BeginRead(buffer, 0, buffer.Length, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data">消息</param>
        public void SendData(string data)
        {
            if (!IsConnected)
            {
                Console.WriteLine($"Client is not connected to a server.");
                return;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(buffer, 0, buffer.Length);
        }
    }
}

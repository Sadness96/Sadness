using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue.Helper.Socket
{
    /// <summary>
    /// WebSocket 客户端帮助类
    /// </summary>
    public class WebSocketClientHelper
    {
        private ClientWebSocket clientWebSocket;
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// 收到数据回调
        /// </summary>
        public event Action<string> OnDataReceived;

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="url">ws://localhost:8080/</param>
        /// <returns></returns>
        public async Task Connect(string url)
        {
            clientWebSocket = new ClientWebSocket();
            cancellationTokenSource = new CancellationTokenSource();

            await clientWebSocket.ConnectAsync(new Uri(url), cancellationTokenSource.Token);

            await Task.Factory.StartNew(async () =>
            {
                while (clientWebSocket.State == WebSocketState.Open)
                {
                    byte[] buffer = new byte[1024];
                    WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationTokenSource.Token);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        OnDataReceived?.Invoke(message);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationTokenSource.Token);
                    }
                }
            }, cancellationTokenSource.Token);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public async Task Send(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationTokenSource.Token);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            cancellationTokenSource.Cancel();
            await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationTokenSource.Token);
        }
    }
}

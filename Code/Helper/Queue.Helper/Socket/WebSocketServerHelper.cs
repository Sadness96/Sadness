using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue.Helper.Socket
{
    /// <summary>
    /// WebSocket 服务端帮助类
    /// </summary>
    public class WebSocketServerHelper
    {
        private HttpListener listener;
        private CancellationTokenSource cancellationTokenSource;
        private List<WebSocket> webSockets;

        /// <summary>
        /// 收到数据回调
        /// </summary>
        public event Action<WebSocket, string> OnDataReceived;

        /// <summary>
        /// 启动
        /// 监听所有 IP 上的端口需要以管理员权限运行
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public async Task Start(int port)
        {
            Console.WriteLine($"WebSocket Server listen port:{port}");

            listener = new HttpListener();
            listener.Prefixes.Add($"http://*:{port}/");
            listener.Start();

            cancellationTokenSource = new CancellationTokenSource();
            webSockets = new List<WebSocket>();

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                HttpListenerContext context = await listener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    ProcessWebSocketRequest(context);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            cancellationTokenSource.Cancel();
            listener.Stop();
        }

        /// <summary>
        /// 处理 WebSocket 请求
        /// </summary>
        /// <param name="context"></param>
        private async void ProcessWebSocketRequest(HttpListenerContext context)
        {
            WebSocketContext webSocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
            WebSocket webSocket = webSocketContext.WebSocket;
            webSockets.Add(webSocket);
            Console.WriteLine($"Add client connection:{context.Request.RemoteEndPoint}");

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    byte[] buffer = new byte[1024];
                    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationTokenSource.Token);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        OnDataReceived.Invoke(webSocket, message);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        webSockets.Remove(webSocket);
                        Console.WriteLine($"Break client connection:{context.Request.RemoteEndPoint}");
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationTokenSource.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                webSockets.Remove(webSocket);
                Console.WriteLine($"Break client connection:{context.Request.RemoteEndPoint}");
            }
        }

        /// <summary>
        /// 发送消息给所有客户端
        /// </summary>
        /// <param name="data">消息</param>
        /// <returns></returns>
        public async Task SendToAllAsync(string data)
        {
            List<Task> tasks = new List<Task>();

            if (webSockets != null && webSockets.Count >= 1)
            {
                foreach (WebSocket webSocket in webSockets)
                {
                    tasks.Add(webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, cancellationTokenSource.Token));
                }

                await Task.WhenAll(tasks);
            }
        }

        /// <summary>
        /// 发送消息给指定客户端
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="data">消息</param>
        /// <returns></returns>
        public async Task SendToClientAsync(WebSocket client, string data)
        {
            try
            {
                await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, cancellationTokenSource.Token);
            }
            catch (Exception)
            {
                // 客户端已断开连接
            }
        }
    }
}

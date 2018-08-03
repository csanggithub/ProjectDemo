using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectDemo.WebAPICore
{
    /// <summary>
    /// WebSocketDto
    /// </summary>
    public class SocketDto
    {
        /// <summary>
        /// 系统标识
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 用户WebSocket
        /// </summary>
        public WebSocket UserSocket { get; set; }
    }

    /// <summary>
    /// WebSocket
    /// </summary>
    public class SocketHandler
    {

        //用户连接池
        private static Dictionary<string, SocketDto> userSocketList = new Dictionary<string, SocketDto>();
        public const int BufferSize = 4096;
        public string basestringjson = string.Empty;

        WebSocket socket;

        SocketHandler(WebSocket socket)
        {
            this.socket = socket;
        }

        async Task EchoLoop(string token)
        {
            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);


            while (this.socket.State == WebSocketState.Open)
            {
                var incoming = await this.socket.ReceiveAsync(seg, CancellationToken.None);
                string userMsg = Encoding.UTF8.GetString(buffer, 0, incoming.Count);
                string userTpye = userMsg.Substring(0, 2);

                byte[] x = Encoding.UTF8.GetBytes(userMsg);
                var outgoing = new ArraySegment<byte>(x);

                foreach (var userSocket in userSocketList)
                {
                    if (userSocket.Value.UserSocket.State == WebSocketState.Open)
                    {
                        if (userSocket.Value.Type == userTpye)
                        {
                            await userSocket.Value.UserSocket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                    //else
                    //{
                    //    userSocketList.Remove(userSocket.Key);
                    //}
                }
            }
        }

        static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            if (!hc.WebSockets.IsWebSocketRequest)
                return;

            var socket = await hc.WebSockets.AcceptWebSocketAsync();

            string token = hc.Request.Query["Token"].ToString();
            string Type = hc.Request.Query["Type"].ToString();

            if (!userSocketList.ContainsKey(token))
            {
                userSocketList.TryAdd(token, new SocketDto
                {
                    Type = Type,
                    UserSocket = socket
                });
            }
            else
            {
                userSocketList[token] = new SocketDto { Type = Type, UserSocket = socket };
            }
            var h = new SocketHandler(socket);
            await h.EchoLoop(token);
        }

        /// <summary>  
        /// branches the request pipeline for this SocketHandler usage  
        /// </summary>  
        /// <param name="app"></param>  
        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(SocketHandler.Acceptor);
        }
    }
}

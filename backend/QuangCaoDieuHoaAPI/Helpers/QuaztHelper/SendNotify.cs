﻿using Quartz;
using QuangCaoDieuHoaAPI.Models;
using System.Net.WebSockets;
using System.Text;

namespace QuangCaoDieuHoaAPI.QuaztHelper
{
    public class SendNotify : IJob
    {
        public SendNotify()
        {
        }

        public Task Execute(IJobExecutionContext context)
        {
            //var connectionList = _connectionManager.GetAllSockets();
            //var connection = default(KeyValuePair<string, WebSocket>);
            //foreach (var item in connectionList)
            //{
            //    var user = _connectionManager.GetUsernameBySocket(item.Value);
            //    if (user == userId.ToString())
            //    {
            //        connection = item;
            //    }
            //}
            //if (!connection.Equals(default(KeyValuePair<string, WebSocket>)))
            //{
            //    var socket = connection.Value;
            //    if (socket.State == WebSocketState.Open)
            //    {
            //        var message = "{\"result\": true}";
            //        await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
            //                                                          offset: 0,
            //                                                          count: message.Length),
            //                           messageType: WebSocketMessageType.Text,
            //                           endOfMessage: true,
            //                           cancellationToken: CancellationToken.None);
            //    }
            //}
            Console.WriteLine("ưueiyri");
            return Task.FromResult(true);
        }

    }
}

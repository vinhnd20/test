﻿using System.Collections.Generic;

namespace QuangCaoDieuHoaAPI.Helpers.SocketHelper
{
    public class ServerMessage
    {
        public ServerMessage()
        {
            Users = new List<string>();
        }

        public ServerMessage(MessageType messageType, string messageContent, List<string> users)
        {
            Type = messageType.ToString();
            Content = messageContent;
            Users = users;
        }

        public ServerMessage(ClientMessage clientMessage, List<string> users)
        {
            Type = clientMessage.GetMessageType();
            Content = clientMessage.BuildChatMessageBody();
            Users = users;
        }

        public ServerMessage(string username, bool isDisconnect, List<string> users)
        {
            Type = MessageType.CONNECTION.ToString();
            Content = BuildConnectionMessageBody(username, isDisconnect);
            Users = users;
        }

        public string Type { get; set; }
        public string Content { get; set; }
        public List<string> Users { get; set; }

        private string BuildConnectionMessageBody(string username, bool isDisconnect)
        {
            if (isDisconnect)
            {
                return $"User {username} left the room.";
            }
            else
            {
                return $"User {username} joined the room.";
            }
        }
    }
}

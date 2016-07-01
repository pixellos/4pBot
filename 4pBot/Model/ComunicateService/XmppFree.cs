using System;
using System.Threading.Tasks;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using pBot.Model.Commands.Helpers;
using pBot.Model.Constants;
using pBot.Model.Core.Data;
using pBot.Model.Core.Abstract;
using pBot.Model.Order;

namespace pBot.Model.ComunicateService
{
    public class XmppFree : IXmpp
    {
        private static string RoomName = "help";

        private readonly XmppClientConnection clientConnection;
        private MucManager mucManager;
        private DateTime startupDate = DateTime.Now;

        public XmppFree()
        {
            clientConnection = new XmppClientConnection
            {
                AutoPresence = true,
                Password = Identity.Password,
                Username = Identity.UserName,
                Server = "4programmers.net"
            };

            clientConnection.Open();
            clientConnection.OnLogin += JoinRoom;
            clientConnection.OnMessage += HandleMessage;

            clientConnection.OnWriteXml += DebugConsoleWrite;
        }

        public ICommandParser Parser { get; set; }
        public OrderDoer Invoker { get; set; }


        public void SendIfNotNull(string message)
        {
            if (message != null)
            {
                clientConnection.Send(new Message
                {
                    Type = MessageType.groupchat,
                    Body = message,
                    To = RoomName + Server(),
                    From = clientConnection.MyJID
                });
            }
        }

        public void PrivateSend(string user, string message)
        {
            clientConnection.Send(
                new Message
                {
                    Type = MessageType.chat,
                    Body = message,
                    To = RoomName + Server() + "/" + user,
                    From = clientConnection.MyJID
                }
                );
        }

        public string ChangeRoom(Command command)
        {
            return ChangeRoom(command.Parameters[0]);
        }
        public string ChangeRoom(string roomName)
        {
            mucManager.LeaveRoom(RoomName + "@conference.4programmers.net", "Bot");

            startupDate = DateTime.Now;

            RoomName = roomName;
            Task.Delay(1000);
            JoinRoom();
            return "Joined";
        }

        private void HandleMessage(object sender, Message msg)
        {
            var Stamp = msg?.XDelay?.Stamp ?? DateTime.Now;

            var nickName = msg?.From.Resource ?? "Undefined";

            if (startupDate < Stamp)
            {
                var response = Invoker.InvokeCommand(nickName,msg.Body);
                if (response != null && msg.Type == MessageType.groupchat)
                {
                    SendIfNotNull(response);
                }
                if (response != null && msg.Type == MessageType.chat)
                {
                    PrivateSend(nickName, response);
                }
            }
        }

        private void DebugConsoleWrite(object sender, string xml)
        {
            Console.WriteLine(xml);
        }

        private void JoinRoom(object sender = null)
        {
            mucManager = new MucManager(clientConnection);
            mucManager.JoinRoom(RoomName + Server(), "Bot");
        }

        private static string Server()
        {
            return "@conference.4programmers.net";
        }
    }
}
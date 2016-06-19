using System;
using pBot.Model.Commands;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using pBot.Model.Commands.Helpers;
using pBot.Model.Core;

namespace pBot.Model.ComunicateService
{
    public class XmppFree : IXmpp
    {
        public ICommandParser Parser { get; set; }
        public ICommandInvoker Invoker { get; set; }

        private XmppClientConnection clientConnection;
        private DateTime startupDate = DateTime.Now;
        private MucManager mucManager;
        private static string RoomName = "help";

        public XmppFree()
        {
            clientConnection = new XmppClientConnection()
            {
                AutoPresence = true,
                Password = "123456",
                Username = "bot4p",
                Server = "4programmers.net",
            };

            clientConnection.Open();
            clientConnection.OnLogin += JoinRoom;
            clientConnection.OnMessage += HandleMessage;
            clientConnection.OnWriteXml += DebugConsoleWrite;
        }

        private void HandleMessage(object sender, Message msg)
        {
            DateTime Stamp = msg?.XDelay?.Stamp ?? DateTime.Now;

            var nickName = msg?.From.Resource ?? "Undefined";

            var command = Parser.GetCommandFromUserNameAndMessage(nickName, msg.Body);
            if (command != Command.Empty() && startupDate < Stamp)
            {
                string response = Invoker.InvokeCommand(command);
                if (response != null)
                {
                    SendIfNotNull(response);
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

        public void SendIfNotNull(string message)
        {
            if (message != null)
            {
                clientConnection.Send(new Message()
                {
                    Type = MessageType.groupchat,
                    Body = message,
                    To = RoomName + Server(),
                    From = clientConnection.MyJID
                });
            }
        }

        private static string Server()
        {
            return "@conference.4programmers.net";
        }

        public void PrivateSend(string user, string message)
        {
            clientConnection.Send(
                new Message()
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
            mucManager.LeaveRoom(RoomName + "@conference.4programmers.net", "Bot");

            RoomName = command.Parameters[0];
            JoinRoom();
            return "Joined";
        }
    }
}

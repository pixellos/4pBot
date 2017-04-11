using System;
using System.Threading.Tasks;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using CoreBot;
using pBot.Model.Constants;
using pBot.Model.Functions;

namespace pBot.Model.ComunicateService
{
    public class XmppFree : IXmpp
    {
        private static string RoomName = "help";
        private readonly XmppClientConnection clientConnection;
        private MucManager mucManager;
        private DateTime startupDate = DateTime.Now;
        public Actions Actions { get; set; }
        public StartupSomethingTodoChangeNameDao Manager { get; set; }
        public void Open() => clientConnection.Open();
        public void Close() => clientConnection.Close();
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
           // clientConnection.OnReadXml += DebugConsoleWrite;
           //  clientConnection.OnPresence += ClientConnection_OnPresence;
          //  clientConnection.OnWriteXml += DebugConsoleWrite;
        }
        
        private void ClientConnection_OnPresence(object sender, Presence pres)
        {
            var g = Manager.Get(pres.From.Resource);
            this.SendIfNotNull(g);    
        }

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
                });
        }

        public string ChangeRoom(string roomName)
        {
            mucManager.LeaveRoom(RoomName + this.Server, "Bot");
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
                
                var response = Actions.InvokeMatchingAction(nickName,msg.Body);
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
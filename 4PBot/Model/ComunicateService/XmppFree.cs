using System;
using System.Threading.Tasks;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using CoreBot;
using _4PBot.Model.Constants;

namespace _4PBot.Model.ComunicateService
{
    public class XmppFree : IXmpp
    {
        private static string RoomName = "help";
        private readonly XmppClientConnection clientConnection;
        private MucManager mucManager;
        private DateTime startupDate = DateTime.Now;
        public Actions Actions { get; set; }
        public void Open() => this.clientConnection.Open();
        public void Close() => this.clientConnection.Close();
        public XmppFree()
        {
            this.clientConnection = new XmppClientConnection
            {
                AutoPresence = true,
                Password = Identity.Password,
                Username = Identity.UserName,
                Server = "4programmers.net"
            };
            this.clientConnection.Open();
            this.clientConnection.OnLogin += this.JoinRoom;
            this.clientConnection.OnMessage += this.HandleMessage;
        }

        private void ClientConnection_OnPresence(object sender, Presence pres)
        {
            //var g = Manager.Get(pres.From.Resource);
            this.SendIfNotNull("NotYetImpl");
        }

        public void SendIfNotNull(string message)
        {
            if (message != null)
            {
                this.clientConnection.Send(new Message
                {
                    Type = MessageType.groupchat,
                    Body = message,
                    To = XmppFree.RoomName + XmppFree.Server(),
                    From = this.clientConnection.MyJID
                });
            }
        }

        public void PrivateSend(string user, string message)
        {
            this.clientConnection.Send(
                new Message
                {
                    Type = MessageType.chat,
                    Body = message,
                    To = XmppFree.RoomName + XmppFree.Server() + "/" + user,
                    From = this.clientConnection.MyJID
                });
        }

        public string ChangeRoom(string roomName)
        {
            this.mucManager.LeaveRoom(XmppFree.RoomName + XmppFree.Server(), "Bot");
            this.startupDate = DateTime.Now;
            XmppFree.RoomName = roomName;
            Task.Delay(1000);
            this.JoinRoom();
            return "Joined";
        }

        private void HandleMessage(object sender, Message msg)
        {
            var Stamp = msg?.XDelay?.Stamp ?? DateTime.Now;
            var nickName = msg?.From.Resource ?? "Undefined";
            if (this.startupDate < Stamp)
            {

                var response = this.Actions.InvokeMatchingAction(nickName, msg.Body);
                if (response != null && msg.Type == MessageType.groupchat)
                {
                    this.SendIfNotNull(response);
                }
                if (response != null && msg.Type == MessageType.chat)
                {
                    this.PrivateSend(nickName, response);
                }
            }
        }

        private void DebugConsoleWrite(object sender, string xml)
        {
            Console.WriteLine(xml);
        }

        private void JoinRoom(object sender = null)
        {
            this.mucManager = new MucManager(this.clientConnection);
            this.mucManager.JoinRoom(XmppFree.RoomName + XmppFree.Server(), "Bot");
        }

        private static string Server()
        {
            return "@conference.4programmers.net";
        }
    }
}
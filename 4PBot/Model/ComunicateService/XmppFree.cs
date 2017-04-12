using System;
using System.Threading.Tasks;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using CoreBot;
using CoreBot.Mask;
using System.Configuration;

namespace _4PBot.Model.ComunicateService
{
    public class XmppFree : IXmpp
    {
        private XmppClientConnection ClientConnection { get; }
        private MucManager MucManager { get; set; }
        private DateTime StartupDate = DateTime.Now;
        private static string Server => ConfigurationManager.AppSettings["XmppServer"];
        private string RoomName = ConfigurationManager.AppSettings["Room"];
        private Actions Actions { get; }

        public void Open() => this.ClientConnection.Open();
        public void Close() => this.ClientConnection.Close();
        public XmppFree(Actions actions)
        {
            this.Actions = actions;
            this.ClientConnection = new XmppClientConnection
            {
                AutoPresence = true,
                Password = ConfigurationManager.AppSettings["Password"],
                Username = ConfigurationManager.AppSettings["UserName"],
                Server = ConfigurationManager.AppSettings["Server"]
            };
            this.ClientConnection.Open();
            this.ClientConnection.OnLogin += this.JoinRoom;
            this.ClientConnection.OnMessage += this.HandleMessage;
            //this.ClientConnection.OnReadXml += this.DebugConsoleWrite;
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
                this.ClientConnection.Send(new Message
                {
                    Type = MessageType.groupchat,
                    Body = message,
                    To = this.RoomName + XmppFree.Server,
                    From = this.ClientConnection.MyJID
                });
            }
        }

        public void PrivateSend(string user, string message)
        {
            this.ClientConnection.Send(new Message
            {
                Type = MessageType.chat,
                Body = message,
                To = this.RoomName + XmppFree.Server + "/" + user,
                From = this.ClientConnection.MyJID
            });
        }

        public string ChangeRoom(string roomName)
        {
            this.MucManager.LeaveRoom(this.RoomName + XmppFree.Server, Builder.Name);
            this.StartupDate = DateTime.Now;
            this.RoomName = roomName;
            this.JoinRoom();
            return "Joined";
        }

        private void HandleMessage(object sender, Message msg)
        {
            var stamp = msg?.XDelay?.Stamp ?? DateTime.Now;
            var nickName = msg?.From.Resource ?? "Undefined";
            if (this.StartupDate.AddSeconds(2) < stamp)
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
            this.MucManager = new MucManager(this.ClientConnection);
            this.MucManager.JoinRoom(this.RoomName + XmppFree.Server, Builder.Name);
        }
    }
}
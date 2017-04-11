using System;
using System.Threading.Tasks;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using CoreBot;
using _4PBot.Model.Constants;
using _4PBot.Model.Functions;
using CoreBot.Mask;
using System.Linq;
using System.Collections.Generic;

namespace _4PBot.Model.ComunicateService
{
    public class XmppFree : IXmpp
    {
        private static string RoomName = "help";
        private XmppClientConnection ClientConnection { get; }
        private MucManager MucManager { get; set; }
        private DateTime StartupDate = DateTime.Now;
        private Actions Actions { get; }

        public void Open() => this.ClientConnection.Open();
        public void Close() => this.ClientConnection.Close();
        public XmppFree(Actions actions)
        {
            this.Actions = actions;
            this.ClientConnection = new XmppClientConnection
            {
                AutoPresence = true,
                Password = Identity.Password,
                Username = Identity.UserName,
                Server = "4programmers.net"
            };
            this.ClientConnection.Open();
            this.ClientConnection.OnLogin += this.JoinRoom;
            this.ClientConnection.OnMessage += this.HandleMessage;
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
                    To = XmppFree.RoomName + XmppFree.Server(),
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
                To = XmppFree.RoomName + XmppFree.Server() + "/" + user,
                From = this.ClientConnection.MyJID
            });
        }

        public string ChangeRoom(string roomName)
        {
            this.MucManager.LeaveRoom(XmppFree.RoomName + XmppFree.Server(), "Bot");
            this.StartupDate = DateTime.Now;
            XmppFree.RoomName = roomName;
            Task.Delay(1000);
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
            this.MucManager.JoinRoom(XmppFree.RoomName + XmppFree.Server(), "Bot");
        }

        private static string Server()
        {
            return "@conference.4programmers.net";
        }
    }
}
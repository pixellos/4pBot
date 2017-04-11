using _4PBot.Model.Functions;
using CoreBot.Mask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreBot;

namespace _4PBot.Model.ComunicateService
{
    public class XmppFreeWithCommandsHandling : XmppFree, ICommand
    {
        public XmppFreeWithCommandsHandling(Actions actions, ICommand[] commands) : base(actions)
        {
            foreach (var item in commands)
            {
                item.Register(actions);
            }
            this.Register(actions);
        }

        enum Words
        {
            Room,
            RoomName,
        }

        public void Register(Actions actions)
        {
            actions[Builder.Bot().Requried(nameof(Words.Room)).ThenWord(nameof(Words.RoomName), nameof(Words.Room)).End()] = result => this.ChangeRoom(result[nameof(Words.RoomName)]);
        }
    }
}

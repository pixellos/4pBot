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
        private enum Words
        {
            Room,
            RoomName,
        }

        public Actions Actions
        {
            get
            {
                var actions = new Actions
                {
                    [Builder.Bot()
                        .Requried(nameof(Words.Room))
                        .ThenWord(nameof(Words.RoomName), nameof(Words.Room))
                        .End()] = result => this.ChangeRoom(result[nameof(Words.RoomName)])
                };
                return actions;
            }
        }

        public XmppFreeWithCommandsHandling(Actions actions) : base(actions)
        {
        }
    }
}

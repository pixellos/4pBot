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
    public class XmppFreeWithCommands : XmppFree, ICommand
    {
        public XmppFreeWithCommands(IEnumerable<ICommand> commands) : base(commands)
        {
        }

        enum Words
        {
            Room,
            RoomName,
            Help
        }

        public Actions AvailableActions
        {
            get
            {
                var actions = new Actions
                {
                    [Builder.Bot().Requried(nameof(Words.Room)).ThenWord(nameof(Words.RoomName), nameof(Words.Help)).End()] = result => this.ChangeRoom(result[nameof(Words.RoomName)])
                };
                return actions;
            }
        }

    }
}

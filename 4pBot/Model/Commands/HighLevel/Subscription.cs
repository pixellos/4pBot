using System.Threading.Tasks;
using pBot.Model.Commands.Helpers;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBot.Model.Commands.HighLevel
{
    public class Subscription : RepeaterBase
    {
        public IXmpp Xmpp { get; set; }

        public Subscription()
        {
            StringAction = (command, s) => Xmpp.PrivateSend(command.Sender, s);
        }
    }
}

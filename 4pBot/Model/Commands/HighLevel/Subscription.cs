using pBot.Model.ComunicateService;
using pBot.Model.Core.Data;

namespace pBot.Model.Commands.HighLevel
{
    public class Subscription : RepeaterBase
    {
        public Subscription()
        {
            StringAction = (command, s) => Xmpp.PrivateSend(command.Sender, s);
        }

        public IXmpp Xmpp { get; set; }
    }
}
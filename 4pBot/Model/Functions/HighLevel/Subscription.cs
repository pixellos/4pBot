using pBot.Model.ComunicateService;

namespace pBot.Model.Functions.HighLevel
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
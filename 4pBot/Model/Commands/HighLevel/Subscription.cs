using System.Threading.Tasks;
using pBot.Model.Commands.Helpers;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBot.Model.Commands.HighLevel
{
    public class Subscription
    {
        public IXmpp Xmpp { get; set; }
        public ICommandInvoker CommandInvoker { get; set; }
        public CachedResponse CachedResponse { get; set; }

        public string CommandSubscribe(Command command)
        {
            Command commandToInvoke = command.GetCommandFromParameters(removeParameters: 1);

            string response = CommandInvoker.InvokeCommand(commandToInvoke);

                CachedResponse.DoWhenResponseIsNotLikeLastResponse(
                    commandToInvoke,
                    response,
                    msg => Xmpp.PrivateSend(command.Sender, msg));

            return "Success";
        }

    }
}

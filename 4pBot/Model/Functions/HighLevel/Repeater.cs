using System;
using System.Threading.Tasks;
using pBot.Model.ComunicateService;
using pBot.Model.Core.Abstract;
using pBot.Model.Core.Cache;
using pBot.Model.Core.Data;
using pBot.Model.Functions.Helpers;

namespace pBot.Model.Functions.HighLevel
{
    public abstract class RepeaterBase
    {
        public const string ErrorNotifyAdminPlease = "Error, notify admin please;";

        protected Action<Command, string> StringAction;
        public ICommandInvoker CommandInvoker { get; set; }
        public CachedResponse CachedResponse { get; set; }


        private static int GetDelayValue(Command command)
        {
            var delayValue = int.Parse(command.Parameters[0])*1000;
            if (int.Parse(command.Parameters[0]) < 1)
            {
                delayValue = 5000;
            }
            return delayValue;
        }

        public string GetCurrentTasks()
        {
            var response = "Current commands and tasks:\n";
            foreach (var keyValuePair in CachedResponse.ReadOnlyCache)
            {
                response +=
                    $"Command action: {keyValuePair.Key.ActionName}, sender: {keyValuePair.Key.Sender}, I: {keyValuePair.Key.Parameters[0]}, II: {keyValuePair.Key.Parameters[1]}\n" +
                    $"Cached response:{keyValuePair.Value}\n";
            }
            return response;
        }

        public string DealWithRepeating(Command rootCommand)
        {
            var childCommand = rootCommand.GetCommandFromParameters();

            if (rootCommand.TypeOfCommand == Command.CommandType.Default ||
                rootCommand.TypeOfCommand == Command.CommandType.Any)
            {
                if (!CachedResponse.ContainsCommand(childCommand))
                {
                    CachedResponse.SetLastResponse(childCommand, "");
                    Task.Run(
                        () => DoRequest(childCommand, GetDelayValue(rootCommand)));
                    return "Request has been Added!";
                }
                return "Request has been already added!";
            }

            if (rootCommand.TypeOfCommand == Command.CommandType.Negation)
            {
                if (CachedResponse.ContainsCommand(childCommand))
                {
                    CachedResponse.Remove(childCommand);
                    return "Request has been removed";
                }
                return "Sorry, there is no matching request";
            }
            return "Never should get there";
        }

        protected void DoRequest(Command command, int Delay)
        {
            while (true)
            {
                var response = CommandInvoker.InvokeCommand(command);

                if (!CachedResponse.ContainsCommand(command))
                {
                    return;
                }
                CachedResponse.DoWhenResponseIsNotLikeLastResponse(command, response, msg => StringAction(command, msg));
            }
        }
    }

    public class Repeater : RepeaterBase
    {
        public Repeater()
        {
            StringAction = (command, msg) => Xmpp.SendIfNotNull(msg);
        }

        public IXmpp Xmpp { get; set; }
    }
}
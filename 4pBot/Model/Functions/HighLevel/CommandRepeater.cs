using System;
using System.Threading.Tasks;
using BotOrder.Abstract.Abstract;
using BotOrder.Old.Core.Cache;
using BotOrder.Old.Core.Data;
using pBot.Model.ComunicateService;
using pBot.Model.Functions.Helpers;

namespace pBot.Model.Functions.HighLevel
{
    public class ContinuesRequest
    {
        public CachedResponse<string,string> CachedResponse { get; set; }

        public string AddRequest(string key, Action action, int delay)
        {
            if (!CachedResponse.ContainsKey(key))
            {
                CachedResponse.InitializeKey(key);
                
                Task.Run(() =>
                {
                    while (CachedResponse.ContainsKey(key))
                    {
                        Task.Delay(delay);
                        action();
                    }    
                });
                return "Request has been added!";
            }
            return "Request already exist";
        }

        public string RemoveRequest(string key)
        {
            if (!CachedResponse.ContainsKey(key))
            {
                return "There is no entry";
            }
            else
            {
                CachedResponse.Remove(key);
                return "OK";
            }
        }
    }

//    public abstract class RepeaterGenericBase<T> where T:class 
//    {
//        public const string ErrorNotifyAdminPlease = "Error, notify admin please;";
//
//        protected Action<T, string> StringAction;
//        public ICommandInvoker<T> CommandInvoker { get; set; }
//        public CachedResponse<T, string> CachedResponse { get; set; }
//
//        public string AddRequest(T t,int delay)
//        {
//            if (!CachedResponse.ContainsKey(t))
//            {
//                CachedResponse.SetLastResponse(t, "");
//                Task.Run(
//                    () => DoRequest(t, delay));
//                return "Request has been Added!";
//            }
//            return "Request has been already added!";
//        }
//
//        protected void DoRequest(T t, int Delay)
//        {
//            while (true)
//            {
//                var response = CommandInvoker.InvokeCommand(t);
//
//                if (!CachedResponse.ContainsKey(t))
//                {
//                    return;
//                }
//                CachedResponse.DoWhenResponseIsNotLikeLastResponse(t , response, msg => StringAction(t , msg));
//            }
//        }
//
//
//    }

    public abstract class RepeaterBase
    {
        public const string ErrorNotifyAdminPlease = "Error, notify admin please;";

        protected Action<Command, string> StringAction;
        public ICommandInvoker CommandInvoker { get; set; }
        public CachedResponse<Command, string> CachedResponse { get; set; }


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

        public string AddRequest(Command childCommand,int delayValue)
        {
            if (!CachedResponse.ContainsKey(childCommand))
            {
                CachedResponse.SetLastResponse(childCommand, "");
                Task.Run(
                    () => DoRequest(childCommand, delayValue));
                return "Request has been Added!";
            }
            return "Request has been already added!";
        }


        public string RemoveRequest(Command command)
        {
            if (CachedResponse.ContainsKey(command))
            {
                CachedResponse.Remove(command);
                return "Request has been removed";
            }
            return "Sorry, there is no matching request";
        }

        public string DealWithRepeating(Command rootCommand)
        {
            var childCommand = rootCommand.GetCommandFromParameters();

            if (rootCommand.TypeOfCommand == Command.CommandType.Default ||
                rootCommand.TypeOfCommand == Command.CommandType.Any)
            {
                return AddRequest(childCommand, GetDelayValue(rootCommand));
            }

            if (rootCommand.TypeOfCommand == Command.CommandType.Negation)
            {
                return RemoveRequest(childCommand);
            }
            return "Never should get there";
        }

        protected void DoRequest(Command command, int Delay)
        {
            while (true)
            {
                var response = CommandInvoker.InvokeCommand(command);

                if (!CachedResponse.ContainsKey(command))
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
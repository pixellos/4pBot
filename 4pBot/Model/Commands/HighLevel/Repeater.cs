using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using pBot.Model.Commands.Helpers;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBot.Model.Commands.HighLevel
{
	public class Repeater
	{
        public const string ErrorNotifyAdminPlease = "Error, notify admin please;";

        public ICommandInvoker CommandInvoker { get; set; }
        public IXmpp Xmpp { get; set; }
	    public CachedResponse CachedResponse { get; set; }

        public ImmutableDictionary<Command,string> ReadonlyCachedResponse => CachedResponse.Cache.ToImmutableDictionary(); 

        private static int GetDelayValue(Command command)
	    {
	        var delayValue = Int32.Parse(command.Parameters[0])*1000;
	        if (Int32.Parse(command.Parameters[0]) < 1)
	        {
	            delayValue = 5000;
	        }
	        return delayValue;
	    }

	    public string GetCurrentTasks()
	    {
	        var response = "Current commands and tasks:\n";
	        foreach (KeyValuePair<Command, string> keyValuePair in CachedResponse.Cache)
	        {
	            response +=
	                $"Command action: {keyValuePair.Key.ActionName}, sender: {keyValuePair.Key.Sender}, I: {keyValuePair.Key.Parameters[0]}, II: {keyValuePair.Key.Parameters[1]}\n" +
	                $"Cached response:{keyValuePair.Value}\n";
	        }
	        return response;
	    }

	    public string DealWithRepeating(Command rootCommand)
	    {
            var childCommand = CommandHelper.GetCommandFromParameters(rootCommand);

            if (rootCommand.TypeOfCommand == Command.CommandType.Default || rootCommand.TypeOfCommand == Command.CommandType.Any )
	        {
	            if (!CachedResponse.Cache.ContainsKey(childCommand))
	            {
	                CachedResponse.InitializeCommand(childCommand);
                    Task.Run(() => AddRequest(childCommand, GetDelayValue(rootCommand)));
                    return "Request has been Added!";
                }
	            return "Request has been already added!";
	        }

	        if (rootCommand.TypeOfCommand == Command.CommandType.Negation)
	        {
	            try
	            {
	                if (CachedResponse.IsAnyInCacheMatchingCommand(childCommand))
	                {
                        CachedResponse.Remove(childCommand);
                        return "Request has been removed";
                    }
                    return "Sorry, there is no matching request";
                }
                catch (Exception e)
	            {
                    Console.WriteLine($"Debug exception {e.Message} was thrown");
	                return ErrorNotifyAdminPlease;
	            }
	        }
	        return "Never should get there";
	    }

        public void AddRequest(Command command, int Delay)
        {
            while (true)
            {
                var response = CommandInvoker.InvokeCommand(command);

                if (!CachedResponse.Cache.ContainsKey(command))
                {
                    return;
                }
                else
                {
                    CachedResponse.DoWhenResponseIsNotLikeLastResponse(command,response,Xmpp.SendIfNotNull);
                }
            }
        }
    }
}


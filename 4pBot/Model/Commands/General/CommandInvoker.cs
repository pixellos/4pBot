using System.Collections.Generic;
using System.Linq;
using static pBot.Model.Commands.CommandDelegates;


namespace pBot.Model.Commands.General
{
	public class CommandInvoker : ICommandInvoker
	{
        
		public void AddTemporaryCommand(Command command, CommandDelegates.CommandAction func)
		{
			actionToCommands.Add(command, func);
		}

        
		Dictionary<Command, CommandAction> actionToCommands = new Dictionary<Command, CommandAction>();

		public string InvokeCommand(Command command)
		{
			var action = actionToCommands.SingleOrDefault(x => x.Key == command).Value;
			if (action == null)
			{
				return "Action not found";
			}

			return action(command);
		}
	}
}


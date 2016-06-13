using System.Collections.Generic;
using System.Linq;
using static pBot.Model.Commands.CommandDelegates;

namespace pBot.Model.Commands.General
{
	public class CommandInvoker : ICommandInvoker
	{
		public CommandInvoker()
		{

		}
		public CommandInvoker(Dictionary<Command, CommandAction> commands)
		{
			commandToAction = commands;
		}

		public void AddTemporaryCommand(Command command, CommandDelegates.CommandAction func)
		{
			commandToAction.Add(command, func);
		}


		Dictionary<Command, CommandAction> commandToAction = new Dictionary<Command, CommandAction>();
	    public static readonly string ActionNotFound = "Action not found";

	    public string InvokeCommand(Command command)
		{
			var action = commandToAction.FirstOrDefault(x => x.Key == command).Value;
			if (action == null)
			{
				return ActionNotFound;
			}

			return action(command);
		}
	}
}


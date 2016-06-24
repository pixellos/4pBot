using System.Collections.Generic;
using System.Linq;

namespace pBot.Model.Core
{
    public class CommandInvoker : ICommandInvoker
    {
        public static readonly string ActionNotFound = "Action not found";

        private readonly Dictionary<Command, CommandDelegates.CommandAction> _commandToCommandActionAction =
            new Dictionary<Command, CommandDelegates.CommandAction>();

        public CommandInvoker(Dictionary<Command, CommandDelegates.CommandAction> commandsToCommandAction)
        {
            _commandToCommandActionAction = commandsToCommandAction;
        }

        public CommandInvoker()
        {
        }

        public void AddTemporaryCommand(Command command, CommandDelegates.CommandAction func)
        {
            _commandToCommandActionAction.Add(command, func);
        }

        public string InvokeCommand(Command command)
        {
            var action = _commandToCommandActionAction.FirstOrDefault(x => x.Key == command).Value;

            if (action == null)
            {
                return ActionNotFound;
            }

            return action(command);
        }
    }
}
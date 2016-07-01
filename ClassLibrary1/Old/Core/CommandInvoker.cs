using System.Collections.Generic;
using System.Linq;
using BotOrder.Abstract.Abstract;

namespace BotOrder.Old.Core
{
    public class CommandInvoker : ICommandInvoker
    {
        public static readonly string ActionNotFound = "Action not found";

        private readonly Dictionary<Data.Command, CommandDelegates.CommandAction> _commandToCommandActionAction =
            new Dictionary<Data.Command, CommandDelegates.CommandAction>();

        public CommandInvoker(Dictionary<Data.Command, CommandDelegates.CommandAction> commandsToCommandAction)
        {
            _commandToCommandActionAction = commandsToCommandAction;
        }

        public CommandInvoker()
        {
        }

        public void AddTemporaryCommand(Data.Command command, CommandDelegates.CommandAction func)
        {
            _commandToCommandActionAction.Add(command, func);
        }

        public string InvokeCommand(Data.Command command)
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
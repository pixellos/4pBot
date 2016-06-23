using System;
using System.Collections.Generic;
using System.Linq;
using pBot.Model.Core;

namespace pBot.Model.Commands.Helpers
{
    public static class CommandHelper
    {
        public static Command GetCommandFromParameters(this Command command,int removeParameters = 2)
        {
            var strArray = new List<string>(command.Parameters);
            strArray.RemoveRange(0,removeParameters);

            return new Command(command.Sender, command.Parameters[removeParameters - 1], Command.CommandType.Default,strArray.ToArray() );
        }

        public static Command MergeCommands(this Command BaseCommand, Command secondaryCommand)
        {
            var mergedParameters = BaseCommand.Parameters.Concat(new []{secondaryCommand.ActionName}).Concat(secondaryCommand.Parameters).ToArray();
            return new Command(BaseCommand.Sender,BaseCommand.ActionName,BaseCommand.TypeOfCommand,mergedParameters);
        }
    }
}
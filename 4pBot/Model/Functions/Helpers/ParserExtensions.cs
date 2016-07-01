using pBot.Model.Core.Abstract;
using pBot.Model.Core.Data;

namespace pBot.Model.Functions.Helpers
{
    public static class ParserExtensions
    {
        public static Command GetCommandFromUserNameAndMessage(this ICommandParser parser, string userName,
            string message)
        {
            var command = parser.GetCommand(userName, message);
            return command;
        }
    }
}
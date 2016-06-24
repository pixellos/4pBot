using pBot.Model.Core;

namespace pBot.Model.Commands.Helpers
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
using System.Text.RegularExpressions;

namespace pBot.Model.Core
{
    class RegexParser : ICommandParser
    {
        public Command GetCommand(string author, string text)
        {
            Regex parser = new Regex(@"\A(\s*)bot(,){0,1}(\s*)(?<negation>don't){0,1}(\s+)(?<Action>[\S]+)(\s+)((?<Parameters>[^\s]+)(\s)*)*"
                , RegexOptions.IgnoreCase);

            var parseResult = parser.Match(text);

            if (! parseResult.Success)
            {
                return Command.Empty();
            }

            string actionString = parseResult.Groups["Action"].Value;
            var isNegation = parseResult.Groups["negation"].Success;
            var parameterGroup = parseResult.Groups["Parameters"];
            var parameterString = GetParameters(parameterGroup);

            return new Command(author,
                actionString, 
                isNegation ? Command.CommandType.Negation : Command.CommandType.Default,
                parameterString);
        }

        private static string[] GetParameters(Group parameterGroup)
        {
            if (! parameterGroup.Success)
            {
                return new string[] { Command.Any };
            }
            int parameterCount = parameterGroup.Captures.Count;

            string[] parameterString = new string[parameterCount];

            for (int i = 0; i < parameterCount; i++)
            {
                parameterString[i] = parameterGroup.Captures[i].Value;
            }
            return parameterString;
        }
    }
}

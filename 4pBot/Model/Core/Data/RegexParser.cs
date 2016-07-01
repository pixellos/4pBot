using System.Text.RegularExpressions;
using pBot.Model.Core.Abstract;

namespace pBot.Model.Core.Data
{
    internal class RegexParser : ICommandParser
    {
        public Core.Data.Command GetCommand(string author, string text)
        {
            var parser =
                new Regex(
                    @"\A(\s*)bot(,){0,1}(\s*)(?<negation>don't){0,1}(\s+)(?<Action>[\S]+)(\s+)((?<Parameters>[^\s]+)(\s)*)*"
                    , RegexOptions.IgnoreCase);

            var parseResult = parser.Match(text);

            if (!parseResult.Success)
            {
                return Core.Data.Command.Empty();
            }

            var actionString = parseResult.Groups["Action"].Value;
            var isNegation = parseResult.Groups["negation"].Success;
            var parameterGroup = parseResult.Groups["Parameters"];
            var parameterString = GetParameters(parameterGroup);

            return new Core.Data.Command(author,
                actionString,
                isNegation ? Core.Data.Command.CommandType.Negation : Core.Data.Command.CommandType.Default,
                parameterString);
        }

        private static string[] GetParameters(Group parameterGroup)
        {
            if (!parameterGroup.Success)
            {
                return new[] {Core.Data.Command.Any};
            }
            var parameterCount = parameterGroup.Captures.Count;

            var parameterString = new string[parameterCount];

            for (var i = 0; i < parameterCount; i++)
            {
                parameterString[i] = parameterGroup.Captures[i].Value;
            }
            return parameterString;
        }
    }
}
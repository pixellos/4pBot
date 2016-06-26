using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pBot.Model.Commands.Parser.Advanced
{
    public class CommandArgument
    {
        public readonly ArgumentOptions ArgumentOptions;
        public readonly string ArgumentName;

        public CommandArgument(ArgumentOptions argumentOptions, string argumentName)
        {
            ArgumentOptions = argumentOptions;
            ArgumentName = argumentName;
        }
    }

    class Result
    {
        private Mask _commandMask;
        private readonly string _fromString;

        public Dictionary<string, string> MatchedResult { get; }

        public Result(Mask commandMask, string fromString, Dictionary<string, string> matchedResult)
        {
            _commandMask = commandMask;
            _fromString = fromString;
            MatchedResult = matchedResult;
        }
    }
}

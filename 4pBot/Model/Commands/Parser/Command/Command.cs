using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pBot.Model.Commands.Parser.Advanced
{
    public class Argument
    {
        public readonly ArgumentOptions ArgumentOptions;
        public readonly string ArgumentName;

        public Argument(ArgumentOptions argumentOptions, string argumentName)
        {
            ArgumentOptions = argumentOptions;
            ArgumentName = argumentName;
        }
    }

    class CommandResult
    {
        private Mask _commandMask;
        private readonly string _fromString;

        public Dictionary<string, string> MatchedResult { get; }

        public CommandResult(Mask commandMask, string fromString, Dictionary<string, string> matchedResult)
        {
            _commandMask = commandMask;
            _fromString = fromString;
            MatchedResult = matchedResult;
        }
    }
}

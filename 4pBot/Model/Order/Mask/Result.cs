using System.Collections.Generic;

namespace pBot.Model.Order.Mask
{
    public class Result
    {
        private readonly Mask _commandMask;
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
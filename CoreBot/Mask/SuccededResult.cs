using System.Collections.Generic;

namespace CoreBot.Mask
{
    public class SuccededResult : Result
    {
        public Dictionary<string, string> MatchedResult { get; }
        public string this[string matchName] => this.MatchedResult[matchName];

        public SuccededResult(Mask commandMask, string fromString, Dictionary<string, string> matchedResult) : base(commandMask, fromString)
        {
            this.MatchedResult = matchedResult;
        }
    }
}
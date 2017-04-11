using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoreBot.Mask
{
    public class Mask
    {
        public string Description { get; }
        public string SampleInput { get; }
        public IReadOnlyList<Argument> NameOfArgument { get; }

        public readonly string RegexString;

        internal Mask(Block block)
        {
            this.RegexString = block.RegexString;
            this.Description = block.Description;
            this.NameOfArgument = block.Arguments;
            this.SampleInput = block.SampleInput;
        }

        public override string ToString()
        {
            return String.Join(", ", this.NameOfArgument);
        }

        public Result Parse(string author, string text)
        {
            var regex = new Regex(this.RegexString, RegexOptions.IgnoreCase);
            var result = regex.Match(text);
            if (result.Success)
            {
                var dict = new Dictionary<string, string>();
                foreach (var argument in this.NameOfArgument)
                {
                    dict.Add(argument.ArgumentName, result.Groups[argument.ArgumentName].Value);
                }
                return new SuccededResult(this, text, dict);
            }
            return new Result(this, text);
        }
    }
}
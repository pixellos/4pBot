using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace pBot.Model.Order.Mask
{
    public class Mask
    {
        public readonly string Description;
        public readonly string SampleInput;

        public readonly IReadOnlyList<Argument> NameOfArgument;

        public readonly string RegexString;

        internal Mask(Block block)
        {
            RegexString = block.RegexString;
            Description = block.Description;
            NameOfArgument = block.Arguments;
            SampleInput = block.SampleInput;
        }

        public override string ToString()
        {
            return string.Join(", ",NameOfArgument);
        }

        public Result Parse(string author, string text)
        {
            Regex regex = new Regex(RegexString, RegexOptions.IgnoreCase);

            var result = regex.Match(text);

            if (result.Success)
            {
                var dict = new Dictionary<string,string>();
                foreach (Argument argument in NameOfArgument)
                {
                    dict.Add(argument.ArgumentName,result.Groups[argument.ArgumentName].Value);
                }
                return new Result(this,text,dict);
            }

            throw new FormatException($"Format of passed {nameof(text)} is invaild");
        }
    }
}
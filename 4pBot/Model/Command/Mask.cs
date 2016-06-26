using System.Collections.Generic;
using System.Linq;
using pBot.Model.Commands.Parser.Advanced;

namespace pBot.Model.Commands.Parser
{
    public class Mask
    {
        public readonly string Description;
        public readonly string SampleInput;

        public readonly IReadOnlyList<CommandArgument> NameOfArgument;

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
    }
}
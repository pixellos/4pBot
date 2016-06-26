using System.Collections.Generic;
using System.Dynamic;
using pBot.Model.Commands.Parser.Advanced;

namespace pBot.Model.Commands.Parser
{
    public class Block
    {
        internal List<CommandArgument> Arguments = new List<CommandArgument>();

        internal string Description = "";
        internal string SampleInput = "";
        internal string RegexString = "";

        internal Block() {}
    }
}
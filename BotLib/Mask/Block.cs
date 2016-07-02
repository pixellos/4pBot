using System.Collections.Generic;

namespace BotOrder.Mask
{
    public class Block
    {
        internal List<Argument> Arguments = new List<Argument>();

        internal string Description = "";
        internal string SampleInput = "";
        internal string RegexString = "";

        internal Block() {}
    }
}
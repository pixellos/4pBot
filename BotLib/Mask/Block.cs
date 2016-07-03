using System.Collections.Generic;

namespace CoreBot.Mask
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
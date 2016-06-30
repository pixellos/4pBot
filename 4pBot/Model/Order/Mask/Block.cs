using System.Collections.Generic;

namespace pBot.Model.Order.Mask
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
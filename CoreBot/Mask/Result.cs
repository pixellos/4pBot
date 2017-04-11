using System;

namespace CoreBot.Mask
{
    public class Result
    {
        public Mask Mask { get; }
        public string Source { get; }

        public Result(Mask commandMask, string fromString)
        {
            this.Mask = commandMask;
            this.Source = fromString;
        }
    }
}
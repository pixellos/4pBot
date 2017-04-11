using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreBot.Mask;
using System.Collections.Immutable;

namespace CoreBot
{
    public class Actions : IEnumerable<KeyValuePair<Mask.Mask, Func<SuccededResult, string>>>
    {
        private readonly Dictionary<Mask.Mask, Func<SuccededResult, string>> Container = new Dictionary<Mask.Mask, Func<SuccededResult, string>>();
        //Todo: I18N
        public const string HelpHeader = "Description\nSample Input\n";
        public const string HorizontalSeparator = "====================\n";

        public Func<SuccededResult, string> this[Mask.Mask mask]
        {
            set { this.Container[mask] = value; }
            get { return this.Container[mask]; }
        }

        public void Add(Mask.Mask mask, Func<SuccededResult, string> func)
        {
            this.Container.Add(mask, func);
        }

        /// <summary>
        /// Return null when result should not be send
        /// Invokes only first matching action
        /// </summary>
        /// <returns>Status response</returns>
        public string InvokeMatchingAction(string author, string text)
        {
            foreach (var record in this.Container)
            {
                var result = record.Key.Parse(author, text);
                if (result is SuccededResult succeded)
                {
                    return record.Value(succeded);
                }
            }
            return null;
        }

        public IEnumerator<KeyValuePair<Mask.Mask, Func<SuccededResult, string>>> GetEnumerator()
        {
            var temp = this.Container.ToImmutableDictionary();
            return temp.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerator<KeyValuePair<Mask.Mask, Func<SuccededResult, string>>>)this);
        }
    }
}

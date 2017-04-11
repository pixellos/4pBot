using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreBot.Mask;

namespace CoreBot
{
    public class Actions : IEnumerable<KeyValuePair<Mask.Mask, Func<SuccededResult, string>>>
    {
        readonly Dictionary<Mask.Mask,Func<SuccededResult, string>> ActionsContainer = new Dictionary<Mask.Mask, Func<SuccededResult, string>>();
        //Todo: I18N
        public const string HelpHeader = "Description\nSample Input\n";
        public const string HorizontalSeparator = "====================\n";

        public Func<SuccededResult, string> this[Mask.Mask mask]
        {
            set { this.ActionsContainer[mask] = value; }
            get { return this.ActionsContainer[mask]; }
        }

        public void Add(Mask.Mask mask, Func<SuccededResult, string> func)
        {
            this.ActionsContainer.Add(mask,func);
        }

        public string GetHelpAboutActions()
        {
            var result = Actions.HelpHeader; 
            foreach (var pair in this.ActionsContainer)
            {
                result += Actions.HorizontalSeparator;
                result += $"{pair.Key.Description}\n";
                result += $"{pair.Key.SampleInput}\n";
            }
            return result;
        }

        /// <summary>
        /// Return null when result should not be send
        /// Invokes only first matching action
        /// </summary>
        /// <returns>Status response</returns>
        public string InvokeMatchingAction(string author, string text)
        {
            foreach (var record in this.ActionsContainer)
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
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreBot.Mask;

namespace CoreBot
{
    public class Actions
    {
        Dictionary<Mask.Mask,Func<Result, string>> ActionsContainer = new Dictionary<Mask.Mask, Func<Result, string>>();
        public const string HelpHeader = "Description\nSample Input\n";
        public const string HorizontalSeparator = "====================\n";

        public Func<Result, string> this[Mask.Mask mask]
        {
            set { ActionsContainer[mask] = value; }
            get { return ActionsContainer[mask]; }
        }

        public void Add(Mask.Mask mask, Func<Result, string> func)
        {
            ActionsContainer.Add(mask,func);
        }

        public string GetHelpAboutActions()
        {
            string result = HelpHeader; 

            foreach (var pair in ActionsContainer)
            {
                result += HorizontalSeparator;
                result += $"{pair.Key.Description}\n";
                result += $"{pair.Key.SampleInput}\n";
            }
            return result;
        }

        /// <summary>
        /// Return null when result should not be send
        /// Invoke only first 
        /// </summary>
        /// <returns>Status response</returns>
        public string InvokeMatchingAction(string author, string text)
        {
            foreach (var record in ActionsContainer)
            {
                try
                {
                    var result = record.Key.Parse(author, text);
                    return record.Value(result);
                }
                catch (FormatException)
                {
                    continue;    
                }
            }
            return null;
        }
    }
}

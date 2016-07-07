using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreBot.Mask;

namespace CoreBot
{
    public class Empty
    { }

    public class SemiActions<TDataContainer> : Actions
    {
        TDataContainer type;

        public SemiActions(TDataContainer dataContainerType)
        {
            type = dataContainerType;
        }
    }

    public class Actions
    {
        Dictionary<Mask.Mask,Func<Result, string>> Dictionary = new Dictionary<Mask.Mask, Func<Result, string>>();
        public const string HelpHeader = "Description\nSample Input\n";
        public const string HorizontalSeparator = "====================\n";

        public Func<Result, string> this[Mask.Mask mask]
        {
            set { Dictionary[mask] = value; }
            get { return Dictionary[mask]; }
        }

        public static Actions operator +(Actions actions, Actions otherActions)
        {
            actions.Merge(otherActions);
            return actions;
        }

        private void Merge(Actions actions)
        {
            Dictionary = Dictionary.Concat(actions.Dictionary).ToDictionary(x=>x.Key,x=>x.Value);
        }

        public void Add<T>(Func<Result, T, string> corelatedAction, Func<T, Mask.Mask> maskCreator, T stringsContainer)
        {
            Dictionary.Add(maskCreator(stringsContainer), x=> corelatedAction(x,stringsContainer));
        }

        public void Add(Mask.Mask mask, Func<Result, string> func)
        {
            Dictionary.Add(mask,func);
        }

        public string GetHelpAboutActions()
        {
            string result = HelpHeader; 

            foreach (var pair in Dictionary)
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
            foreach (var record in Dictionary)
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

using System;
using System.Collections.Generic;
using System.Linq;
using BotOrder.Mask;

namespace BotOrder
{
    public class Orderer
    {
        Dictionary<Mask.Mask,Func<Result, string>> Dictionary = new Dictionary<Mask.Mask, Func<Result, string>>();
        public const string HelpHeader = "Description\nSample Input\n";
        public const string HorizontalSeparator = "====================\n";

        public void AddTemporaryCommand(Mask.Mask mask, Func<Result, string> func)
        {
            Dictionary.Add(mask,func);
        }

        public string GetHelpAboutAllCommands()
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
        public string InvokeConnectedAction(string author, string text)
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

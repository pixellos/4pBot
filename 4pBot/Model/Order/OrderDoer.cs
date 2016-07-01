using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pBot.Model.Order.Mask;

namespace pBot.Model.Order
{
    public class OrderDoer
    {
        Dictionary<Mask.Mask,Func<Result,string>> Dictionary = new Dictionary<Mask.Mask, Func<Result, string>>();

        public void AddTemporaryCommand(Mask.Mask mask, Func<Result, string> func)
        {
            Dictionary.Add(mask,func);
        }


        public string GetHelpAboutAllCommands()
        {
            return Dictionary.Aggregate("", (current, func) => current + ($"Description: {func.Key.Description} ||" + $"Sample input: {func.Key.SampleInput}\n"));
        }

        /// <summary>
        /// Return null when result should not be send
        /// </summary>
        /// <returns>Status response</returns>
        public string InvokeCommand(string author, string text)
        {
            foreach (var record in Dictionary)
            {
                try
                {
                    var result = record.Key.Parse(author, text);
                    return record.Value(result);
                }
                catch (FormatException exception)
                {
                    continue;    
                }
            }

            return null;
        }
    }
}

﻿using System;
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

        public void AddMask(Mask.Mask mask, Func<Result, string> func)
        {
            Dictionary.Add(mask,func);
        }

        /// <summary>
        /// Return null when result should not be send
        /// </summary>
        /// <returns>Status response</returns>
        public string CheckMessage(string author, string text)
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

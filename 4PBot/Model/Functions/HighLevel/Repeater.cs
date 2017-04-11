using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _4PBot.Model.ComunicateService;
using _4PBot.Model.DataStructures;

namespace _4PBot.Model.Functions.HighLevel
{
    public class Repeater
    {
        public CachedResponse<string,string> CachedResponse { get; set; }  = new CachedResponse<string, string>();

        public string CheckRequests()
        {
            var x = "";
            foreach (KeyValuePair<string, string> keyValuePair in this.CachedResponse.ReadOnlyCache)
            {
                x += $"{keyValuePair.Key} : {keyValuePair.Value}";
            }
            return x;
        }

        public string Add(string key, int delay, Func<string> action)
        {
            if (delay < 5)
            {
                delay = 5;
            }
            return this.Add(delay, key, action);
        }

        public string Add(int delay, string key, Func<string> action)
        {
            return "Tempoorary off";
            //if (!this.CachedResponse.ContainsKey(key))
            //{
            //    this.CachedResponse.InitializeKey(key,"");
                
            //    Task.Run(async () =>
            //    {
            //        while (this.CachedResponse.ContainsKey(key))
            //        {
            //            this.CachedResponse.DoWhenResponseIsNotLikeLastResponse(key,action(),this.SendCommand,"");
            //            await Task.Delay((delay > 1 ? delay : 1) * 1000);

            //        }
            //    });
            //    return "Request has been added!";
            //}
            return "Request already exist";
        }

        public string RemoveRequest(string key)
        {
            if (this.CachedResponse.ContainsKey(key))
            {
                this.CachedResponse.Remove(key);
                return "OK";
            }
            return "There is no entry";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pBot.Model.Functions.HighLevel
{
    public class Repeater
    {
        public CachedResponse<string,string> CachedResponse { get; set; } 

        public Action<string> SendCommand { get; set; }

        public string CheckRequests()
        {
            var x = "";
            foreach (KeyValuePair<string, string> keyValuePair in CachedResponse.ReadOnlyCache)
            {
                x += $"{keyValuePair.Key} : {keyValuePair.Value}";
            }
            return x;
        }

        public string AddRequest(string key, Func<string> action, int delay = 5)
        {
            if (!CachedResponse.ContainsKey(key))
            {
                CachedResponse.InitializeKey(key,"");
                
                Task.Run(() =>
                {
                    while (CachedResponse.ContainsKey(key))
                    {
                        Task.Delay((delay > 1 ? delay : 1)   * 1000);
                        CachedResponse.DoWhenResponseIsNotLikeLastResponse(key,action(),SendCommand,"");
                    }    
                });
                return "Request has been added!";
            }
            return "Request already exist";
        }

        public string RemoveRequest(string key)
        {
            if (CachedResponse.ContainsKey(key))
            {
                CachedResponse.Remove(key);
                return "OK";
            }
                return "There is no entry";
        }
    }

}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using pBot.Model.ComunicateService;
using pBot.Model.DataStructures;

namespace pBot.Model.Functions.HighLevel
{
    public class Repeater
    {
        public CachedResponse<string,string> CachedResponse { get; set; }  = new CachedResponse<string, string>();
        public IXmpp Xmpp { get; set; }
        public Action<string> SendCommand => Xmpp.SendIfNotNull;

        public string CheckRequests()
        {
            var x = "";
            foreach (KeyValuePair<string, string> keyValuePair in CachedResponse.ReadOnlyCache)
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
            return Add(delay, key, action);
        }

        public string Add(int delay, string key, Func<string> action)
        {
            if (!CachedResponse.ContainsKey(key))
            {
                CachedResponse.InitializeKey(key,"");
                
                Task.Run(async () =>
                {
                    while (CachedResponse.ContainsKey(key))
                    {
                        CachedResponse.DoWhenResponseIsNotLikeLastResponse(key,action(),SendCommand,"");
                        await Task.Delay((delay > 1 ? delay : 1) * 1000);

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
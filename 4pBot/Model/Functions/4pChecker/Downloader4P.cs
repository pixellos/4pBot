using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace pBot.Model.Functions._4pChecker
{
    internal class Downloader4P
    {
        private readonly string _4pAdress = ApiKey.AdressWithKey;

        private string MagicWithJson(string json)
        {
            return $"{"{ \"Main\":"} {json} {"}"}";
        }

        public RootObject GetData(string jsonForumId)
        {
            var json = new WebClient().DownloadString(_4pAdress + jsonForumId);

            return JsonConvert.DeserializeObject<RootObject>(MagicWithJson(json));
        }
    }
}

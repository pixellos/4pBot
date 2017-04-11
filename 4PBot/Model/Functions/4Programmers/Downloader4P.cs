using System.Net;
using Newtonsoft.Json;
using _4PBot.Model.Constants;

namespace _4PBot.Model.Functions._4Programmers
{
    public class Downloader
    {
        private readonly string _4pAddress = ApiKey.ApiKeyWithForumIdQuotation;

        private string PrepareDownloadedJson(string json)
        {
            return $"{"{ \"Main\":"} {json} {"}"}";
        }

        public virtual Rootobject DownloadData(string jsonForumId,string apiKeyUrl)
        {
            var json = new WebClient().DownloadString(apiKeyUrl + jsonForumId);
            return this.Deserialize(this.PrepareDownloadedJson(json));
        }

        public Rootobject Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Rootobject>(json);
        }
    }
}

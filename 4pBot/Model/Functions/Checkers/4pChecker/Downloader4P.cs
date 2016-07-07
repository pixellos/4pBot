using System.Net;
using Newtonsoft.Json;
using pBot.Model.Constants;

namespace pBot.Model.Functions.Checkers._4pChecker
{
    public class Downloader4P
    {
        private readonly string _4pAddress = ApiKey.ApiKeyWithForumIdQuotation;

        private string PrepareDownloadedJson(string json)
        {
            return $"{"{ \"Main\":"} {json} {"}"}";
        }

        public virtual RootObject DownloadData(string jsonForumId,string apiKeyUrl)
        {
            var json = new WebClient().DownloadString(apiKeyUrl + jsonForumId);
            return Deserialize(PrepareDownloadedJson(json));
        }

        public RootObject Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<RootObject>(json);
        }
    }
}

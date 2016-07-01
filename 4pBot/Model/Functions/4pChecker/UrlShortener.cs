using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;
using pBot.Model.Constants;

namespace pBot.Model.Functions._4pChecker
{
    public static class UrlShortener
    {
        public static string GetShortUrl(string longUrl)
        {
            var url = new Url();
            url.LongUrl = longUrl;
            var returnValue =
                new UrlshortenerService(new BaseClientService.Initializer
                {
                    ApiKey = Identity.GoogleApiKey
                })
                .Url.Insert(url).Execute();

            return returnValue.Id;
        }
    }
}
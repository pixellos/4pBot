using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;

namespace pBot.Model.Commands._4pChecker
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
                    ApiKey = "AIzaSyDxVEUf6ZXnRckEnZeLTpHw5bVA5YORqNk"
                }).Url.Insert(url).Execute();

            return returnValue.Id;
        }
    }
}
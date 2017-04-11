using System;
using System.Linq;
using System.Web;
using pBot.Model.Helper;

namespace pBot.Model.Functions.Checkers.SOChecker
{
    public class Checker
    {
        public static readonly string CantFindRequestMessage = "Sorry, I can't find your request :(";
        private Downloader DownloaderSo { get; }
        public Checker(Downloader downloader)
        {
            this.DownloaderSo = downloader;
        }

        public string CheckNewestByTag(string tagName)
        {
            var html = DownloaderSo.Download(tagName);
            try
            {
                var question = html.DocumentNode
                    .Descendants().First(node => node.GetAttributeValue("class", "").Equals("question-summary"));
                var firstQuestion =
                    question.Descendants().First(x => x.GetAttributeValue("class", "").Equals("question-hyperlink"));
                return $"{HttpUtility.HtmlDecode(firstQuestion.InnerText)} {UrlShortener.GetShortUrl($"www.stackoverflow.com{firstQuestion.Attributes["href"].Value}")}";
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine($"{exception.Message} at StackOverFlowChecker");
                return CantFindRequestMessage;
            }
        }
    }
}
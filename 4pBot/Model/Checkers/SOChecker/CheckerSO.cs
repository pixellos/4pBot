using System;
using System.Linq;
using System.Web;
using pBot.Model.Functions.Helper;

namespace pBot.Model.Functions.Checkers.SOChecker
{
    public class CheckerSO
    {
        public const string CantFindRequestMessage = "Sorry, I can't find your request :(";

        public DownloaderSo DownloaderSo { get; set; }

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
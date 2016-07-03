using System;
using System.Linq;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using pBot.Model.Functions._4pChecker;
namespace pBot.Model.Functions.StackOverflowChecker
{
    public class StackOverflowHtmlChecker
    {
        public string GetSingleSORequestWithTagAsParameter(string unescapedTag)
        {
            var html = new HtmlDocument();
            var escapedTag = HttpUtility.HtmlEncode(unescapedTag);

            html.LoadHtml(
                new WebClient().DownloadString($"http://stackoverflow.com/questions/tagged/{escapedTag}"));
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
                return "Sorry, I can't find your request :(";
            }
        }
    }
}
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using pBot.Model.Core;

namespace pBot.Model.Commands.StackOverflowChecker
{
    public class StackOverflowHtmlChecker
    {
        public static string GetSingleSORequestWithTagAsParameter(Command command)
        {
            var html = new HtmlDocument();
            var escapedTag = command.Parameters[1].Replace("#","%23");

            html.LoadHtml(
                new WebClient().DownloadString($"http://stackoverflow.com/questions/tagged/{escapedTag}"));

            var question = html.DocumentNode.Descendants()
                .Where(node => node.GetAttributeValue("class", "").Equals("question-summary")).First();

            var firstQuestion =question.Descendants().Where(x => x.GetAttributeValue("class", "").Equals("question-hyperlink")).First();

            return $"{firstQuestion.InnerText}, [www.stackoverflow.com{firstQuestion.Attributes["href"].Value}]";
        }
    }
}
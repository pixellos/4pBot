using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using pBot.Model.Core;
using static System.Web.HttpUtility;

namespace pBot.Model.Commands.StackOverflowChecker
{
    public class StackOverflowHtmlChecker
    {
        public static string GetSingleSORequestWithTagAsParameter(Command command)
        {
            var html = new HtmlDocument();
            
            var escapedTag = HtmlEncode(command.Parameters[1]);

            html.LoadHtml(
                new WebClient().DownloadString($"http://stackoverflow.com/questions/tagged/{escapedTag}"));
            try
            {
                var question = html.DocumentNode.Descendants()
                .Where(node => node.GetAttributeValue("class", "").Equals("question-summary")).First();
          
                var firstQuestion = question.Descendants().First(x => x.GetAttributeValue("class", "").Equals("question-hyperlink"));
                return $"{firstQuestion.InnerText}, [www.stackoverflow.com{firstQuestion.Attributes["href"].Value}]";
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine($"{exception.Message} at StackOverFlowChecker" );
                return "Sorry, I can't find your request :(";
            }
        }
    }
}
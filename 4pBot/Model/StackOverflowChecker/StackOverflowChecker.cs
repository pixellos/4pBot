using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using pBot.Model.Commands;
using StackExchange.StacMan;

namespace pBot.Model.StackOverflowChecker
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

	public class StackOverflowChecker
	{
		public StackOverflowChecker()
		{
			var clinet = new StacManClient();
		    clinet.MaxSimultaneousRequests = 1;
        }

		public static string GetSingleSORequestWithTagAsParameter(Command command)
		{
			var clinet = new StacManClient();

			string Tags = "";
		    var parametersWithoutCommandConcretizeParameter = command.Parameters.Skip(1);

			foreach (string item in parametersWithoutCommandConcretizeParameter)
			{
				Tags += " " + item ;
			}

			var response = clinet.Questions.GetWithNoAnswers("stackoverflow", tagged: Tags, order: Order.Desc, page: 1, pagesize: 1).Result;

		    if (response.Data.Items == null)
		    {
		        return response.Error.Message;
		    }

			var first = response.Data.Items.FirstOrDefault();

		    if (first == null)
		    {
		        return "Sorry, i cant find anything.";
		    }

			return $"[{first.Title}] {first.Link} ; Answers: {first.AnswerCount}; Tags {Tags}";
		}
	}
}


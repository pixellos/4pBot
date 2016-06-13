using System;
using StackExchange.StacMan;
using System.Linq;
using pBot.Model.Commands;
using System.Text;
using GLib;
using System.Collections.Generic;

namespace pBot
{
	public class StackOverflowChecker
	{
		public StackOverflowChecker()
		{
			var clinet = new StacManClient();

			var response = clinet.Questions.GetWithNoAnswers("stackoverflow", tagged: "C#", order: Order.Desc, page: 1, pagesize: 1).Result;
			var first = response.Data.Items.First();
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
			var first = response.Data.Items.FirstOrDefault();

		    if (first == null)
		    {
		        return "Sorry, i cant find anything.";
		    }

			return $"[{first.Title}] {first.Link} ; Answers: {first.AnswerCount}; Tags {Tags}";
		}
	}
}


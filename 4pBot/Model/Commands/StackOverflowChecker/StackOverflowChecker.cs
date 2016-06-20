using System.Linq;
using pBot.Model.Core;
using StackExchange.StacMan;

namespace pBot.Model.Commands.StackOverflowChecker
{
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


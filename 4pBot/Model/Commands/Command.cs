using System;
namespace pBot.Model.Commands
{

	public class Command
	{
		public string Sender { get; }
		public bool IsNegation { get; }
		public string ActionName { get; }
		public string[] Parameters { get; }

		public Command(string sender, string actionName, bool isNegation, params string[] parameters)
		{
			Sender = sender;
			IsNegation = isNegation;
			ActionName = actionName;
			Parameters = parameters;
		}
	}
}

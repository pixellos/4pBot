using System;
namespace pBot
{

	public class MessageStructure
	{
		public string Sender { get; }
		public string ActionName { get; }
		public string[] Parameters { get; }

		public MessageStructure(string sender, string actionName, params string[] parameters)
		{
			Sender = sender;
			ActionName = actionName;
			Parameters = parameters;
		}

	}

}

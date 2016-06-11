using System;
using System.Linq;

namespace pBot.Model.Commands
{
	/// <summary>
	/// Empty string mean any value
	/// </summary>
	public class Command
	{
		public static Command Empty()
		{
			return new Command("", "", false);
		}

		public static string Any => String.Empty;

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

	    public static bool operator ==(Command command1, Command command2)
	    {
	        bool isActionNamesEquals = command1.ActionName.Equals(command2.ActionName);
	        bool isNegationEquals = command1.IsNegation.Equals(command2.IsNegation);
	        bool isSenderEquals = command1.Sender.Equals(command2.Sender) || command1.Sender.Equals(Any) ||
	                              command2.Sender.Equals(Any);

	        return isSenderEquals && isNegationEquals && isActionNamesEquals;
	    }

	    public static bool operator !=(Command command1, Command command2)
	    {
	        return !(command1 == command2);
	    }

	    public override bool Equals(object obj)
	    {
	        if (obj is Command)
	        {
	            return this == (Command) obj;
	        }
	        return base.Equals(obj);
	    }
	}
}


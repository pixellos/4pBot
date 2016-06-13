using System;
using System.Linq;
using Matrix.Dns.Managed;

namespace pBot.Model.Commands
{
	/// <summary>
	/// Empty string mean any value
	/// </summary>
	public class Command
	{
	    private readonly static Command EmptyCommand = new Command("", "", CommandType.Default);

        public static Command Empty()
		{
			return EmptyCommand;
		}

		public static string Any => String.Empty;

	    public enum CommandType
	    {
            Default,
	        Negation,
            Any
	    }

		public string Sender { get; }
		public CommandType TypeOfCommand{ get; }
		public string ActionName { get; }
		public string[] Parameters { get; }

		public Command(string sender, string actionName, CommandType commandType, params string[] parameters)
		{
			Sender = sender;
			TypeOfCommand = commandType;
			ActionName = actionName;
			Parameters = parameters;
		}

	    public static bool operator ==(Command command1, Command command2)
	    {
	        bool isActionNamesEquals = command1?.ActionName.Equals(command2?.ActionName,StringComparison.OrdinalIgnoreCase) ?? false;

	        bool isNegationEquals = command1.TypeOfCommand.Equals(command2.TypeOfCommand)
                || command1.TypeOfCommand == CommandType.Any
                || command2.TypeOfCommand == CommandType.Any;

	        bool isSenderEquals = command1.Sender.Equals(command2.Sender, StringComparison.OrdinalIgnoreCase) || command1.Sender.Equals(Any) ||
	                              command2.Sender.Equals(Any);
	        bool areParametersEqual = true;

	        if (command1.Parameters.Length != command2.Parameters.Length)
	        {
	            return false;
	        }

	        for (int i = 0; i < command1.Parameters.Count(); i++)
	        {
	            if (command1.Parameters[i] == null || command2.Parameters[i] == null)
	            {
	                areParametersEqual = false;
	                break;
	            }
	            if (command1.Parameters[i] == Any || command2.Parameters[i] == Any)
	            {
	                continue;
	            }

                areParametersEqual = command1.Parameters[i].Equals(command2.Parameters[i],StringComparison.OrdinalIgnoreCase);
	        }

	        return isSenderEquals && isNegationEquals && isActionNamesEquals && areParametersEqual;
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


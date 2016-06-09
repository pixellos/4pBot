using System;
namespace pBot.Model.Commands
{
	/// <summary>
	/// * string should mean any
	/// </summary>
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

		public override bool Equals(object obj)
		{
			if (obj is Command)
			{
				return ((Command)obj).ActionName.Equals(this.ActionName)
									 && ((Command)obj).IsNegation == IsNegation
									 && ((Command)obj).Parameters.Equals(Parameters)
									 && ((Command)obj).Sender.Equals(Sender);

			}
			return base.Equals(obj);
		}
	}
}

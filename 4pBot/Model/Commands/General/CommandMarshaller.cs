using System;
using System.Linq;
using pBot.Commands;
using pBot.Model;
using pBot.Model.Commands;
using System.Collections.Generic;

namespace pBot.Commands.General
{
	public class CommandMarshaller : ICommandMarshaller
	{
		public CommandMarshaller()
		{
		}

		public const string NegateString = "don't";

		public Command GetCommand(string text)
		{

			var splittedText = text.Split(' ').ToList();

			if (splittedText[1].Equals("Bot,"))
			{
				splittedText.RemoveAt(1);
			}
			else
			{
				return Command.Empty();
			}

			bool isNegate = false;
			if (splittedText.Remove(NegateString))
			{
				isNegate = true;
			}

			var user = GetUser(splittedText);
			var action = GetAction(splittedText);
			var parameters = GetParams(splittedText);
			return new Command(
				user,
				action,
				isNegate,
				parameters);
		}

		string[] GetParams(List<string> commandStrings)
		{
			return commandStrings.ToArray();
		}

		string GetAction(List<string> commandStrings)
		{
			var temp = commandStrings[0];
			commandStrings.RemoveAt(0);
			return temp;
		}

		string GetUser(List<string> commandStrings)
		{
			var user = commandStrings[0];
			commandStrings.RemoveAt(0);

			user = user.PadLeft(1);
			return user.PadRight(2);
		}

	}
}


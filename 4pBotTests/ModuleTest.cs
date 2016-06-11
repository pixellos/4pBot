using NUnit.Framework;
using System;
using pBot.Model.Commands;
namespace pBotTests
{
	[TestFixture()]
	public class Test
	{
		ICommandMarshaller CommandMarshaler;
		ICommandInvoker CommandInvoker;

		public string TestCase(string command)
		{
			var Command = CommandMarshaler.GetCommand(command);

			var result = CommandInvoker.InvokeCommand(Command);

			return result;
		}
	}
}


using NUnit.Framework;
using System;
using pBot.Model.Commands;
namespace pBotTests
{
	[TestFixture()]
	public class CommandInvokerTest
	{
		Command command = new Command("Test", "TestAction", false);
		ICommandInvoker commandInvoker;
		public CommandInvokerTest()
		{
			commandInvoker.InvokeCommand(command)
		}
	}
}


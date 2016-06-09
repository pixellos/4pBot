using NUnit.Framework;
using System;
using pBot.Model.Commands;
namespace pBotTests
{
	[TestFixture()]
	public class MyCommandInvokerTest
	{
		Command command = new Command("Test", "TestAction", false);
		string ExpectedResult = "Test";

		ICommandInvoker commandInvoker;
		[Test]
		public void InvokeTemporaryCommand()
		{
			commandInvoker.AddTemporaryCommand(command, x => { return ExpectedResult; });

			Assert.Equals(commandInvoker.InvokeCommand(command), ExpectedResult);
		}
	}
}


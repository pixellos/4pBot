using NUnit.Framework;
using System;
using pBot.Model.Commands;
using static pBot.Model.Commands.CommandDelegates;
using Autofac;

namespace pBotTests
{
	[TestFixture()]
	public class MyCommandInvokerTest
	{
		Command Test_Command = new Command("Test", "TestAction", false);
		string ExpectedResult = "Test";

	    [SetUp]
	    public void Setup()
	    {
            commandInvoker = pBot.AutofacSetup.GetContainer().Resolve<ICommandInvoker>();
        }
        
		ICommandInvoker commandInvoker;

		[Test, TestCaseSource(typeof(InvokerDataForTest), "TestCases")]
		public string CheckResult_TemporaryCommands(Command command, CommandAction str)
		{
			commandInvoker.AddTemporaryCommand(command, str);
			return commandInvoker.InvokeCommand(command);
		}


        [Test, TestCaseSource(typeof(InvokerDataForTest), "TestCases")]
        public string CheckResult_TemporaryCommands_WithPreloadedElement(Command command, CommandAction str)
        {
            commandInvoker.AddTemporaryCommand(Test_Command,x=>ExpectedResult);
            commandInvoker.AddTemporaryCommand(command, str);

            var returnValue = commandInvoker.InvokeCommand(Test_Command);

            Assert.AreEqual(returnValue,ExpectedResult);
            return commandInvoker.InvokeCommand(command);
        }


    }
}


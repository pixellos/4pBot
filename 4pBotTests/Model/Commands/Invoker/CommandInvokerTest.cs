using Autofac;
using NUnit.Framework;
using pBot.Dependencies;
using pBot.Model.Commands;
using pBot.Model.Core;

namespace pBotTests.Model.Commands.Invoker
{
	[TestFixture()]
	public class MyCommandInvokerTest
	{
		Command Test_Command = new Command("Test", "TestAction", Command.CommandType.Negation);
		string ExpectedResult = "Test";
        ICommandInvoker commandInvoker;

        [SetUp]
	    public void Setup()
	    {
            commandInvoker = AutofacSetup.GetContainer().Resolve<ICommandInvoker>();
        }
        
		[Test, TestCaseSource(typeof(InvokerDataForTest), "TestCases")]
		public string CheckResult_TemporaryCommands(Command command, CommandDelegates.CommandAction str)
		{
			commandInvoker.AddTemporaryCommand(command, str);
			return commandInvoker.InvokeCommand(command);
		}

        [Test, TestCaseSource(typeof(InvokerDataForTest), "TestCases")]
        public string CheckResult_TemporaryCommands_WithPreloadedElement(Command command, CommandDelegates.CommandAction str)
        {
            commandInvoker.AddTemporaryCommand(Test_Command,x=>ExpectedResult);
            commandInvoker.AddTemporaryCommand(command, str);

            var returnValue = commandInvoker.InvokeCommand(Test_Command);

            Assert.AreEqual(returnValue,ExpectedResult);
            return commandInvoker.InvokeCommand(command);
        }
    }
}


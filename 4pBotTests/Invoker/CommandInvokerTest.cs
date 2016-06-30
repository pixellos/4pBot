using Autofac;
using NUnit.Framework;
using pBot.Dependencies;
using pBot.Model.Core;
using pBot.Model.Core.Abstract;
using pBot.Model.Core.Data;

namespace pBotTests.Invoker
{
    [TestFixture]
    public class MyCommandInvokerTest
    {
        [SetUp]
        public void Setup()
        {
            commandInvoker = AutofacSetup.GetContainer().Resolve<ICommandInvoker>();
        }

        private readonly Command Test_Command = new Command("Test", "TestAction", Command.CommandType.Negation);
        private readonly string ExpectedResult = "Test";
        private ICommandInvoker commandInvoker;

        [Test, TestCaseSource(typeof (InvokerDataForTest), "TestCases")]
        public string CheckResult_TemporaryCommands(Command command, CommandDelegates.CommandAction str)
        {
            commandInvoker.AddTemporaryCommand(command, str);
            return commandInvoker.InvokeCommand(command);
        }

        [Test, TestCaseSource(typeof (InvokerDataForTest), "TestCases")]
        public string CheckResult_TemporaryCommands_WithPreloadedElement(Command command,
            CommandDelegates.CommandAction str)
        {
            commandInvoker.AddTemporaryCommand(Test_Command, x => ExpectedResult);
            commandInvoker.AddTemporaryCommand(command, str);

            var returnValue = commandInvoker.InvokeCommand(Test_Command);

            Assert.AreEqual(returnValue, ExpectedResult);
            return commandInvoker.InvokeCommand(command);
        }
    }
}
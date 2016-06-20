using Autofac;
using NUnit.Framework;
using pBot.Dependencies;
using pBot.Model.Commands;
using pBot.Model.Commands.HighLevel;
using pBot.Model.Core;

namespace pBotTests.Model.Commands.HighLevel
{
    [TestFixture()]
    public class RepeaterTests
    {
        private Repeater repeater;

        static Command MockCommand = new Command("Test", "Test", Command.CommandType.Default,"Test");
        private static string MockResponse = "TestResponse";

        static Command ItReturnsMockCommand()
        {
            return MockCommand;
        }

        [SetUp]
        public void Setup()
        {
            repeater = AutofacSetup.GetContainer().Resolve<Repeater>();

            repeater.CommandInvoker.AddTemporaryCommand(MockCommand,(x)=> MockResponse);

        }



    }
}


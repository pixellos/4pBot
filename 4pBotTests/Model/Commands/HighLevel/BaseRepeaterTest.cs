using System;
using Autofac;
using NSubstitute;
using NUnit.Framework;
using pBot.Dependencies;
using pBot.Model.Commands.HighLevel;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBotTests.Model.Commands.HighLevel
{
    [TestFixture()]
    public class BaseRepeaterTests
    {
        private RepeaterBase repeater;

        static Command MockCommand = new Command("Test", "Test", Command.CommandType.Default,"Test");
        private static string MockResponse = "TestResponse";

        static Command ItReturnsMockCommand()
        {
            return MockCommand;
        }

        [SetUp]
        public void Setup()
        {
            repeater = new Repeater() {CachedResponse = new CachedResponse(),CommandInvoker = new CommandInvoker(), Xmpp = Substitute.For<IXmpp>()};
        }

        [Test]
        public void ThrowsExceptionWhenCommandIsntHighLevel()
        {
            Assert.Throws<System.ArgumentException>(
                () =>
                {
                    repeater.DealWithRepeating(MockCommand);
                });
        }

        Command RepeatingCommand = new Command("Someone","Auto",Command.CommandType.Default,"5","Test","Test","Test"); // Inside is MockCommand
        Command AnotherRepeatingCommand = new Command("Someone","Auto",Command.CommandType.Default,"5","Test","Test","NotTest"); // Inside is MockCommand

        [Test]
        public void CheckRepeatingTasksLineCountForOneDeal()
        {
            int expectedLines = 4; //2 for help, 2 per "Deal"

            repeater.DealWithRepeating(RepeatingCommand);
            var countOfLines = repeater.GetCurrentTasks().Split('\n').Length;

            Assert.AreEqual(expectedLines, countOfLines, repeater.GetCurrentTasks());
        }

        [Test]
        public void CheckRepeatingTasksLineCountFor2Deals()
        {
            int expectedLines = 6; //2 for help, 2 per "Deal"
            repeater.DealWithRepeating(RepeatingCommand);
            repeater.DealWithRepeating(AnotherRepeatingCommand);

            var countOfLines = repeater.GetCurrentTasks().Split('\n').Length;

            Assert.AreEqual(expectedLines,countOfLines,repeater.GetCurrentTasks());
        }

 
    }
}


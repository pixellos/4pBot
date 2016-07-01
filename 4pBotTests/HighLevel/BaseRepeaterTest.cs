using System;
using NSubstitute;
using NUnit.Framework;
using pBot.Model.Commands.HighLevel;
using pBot.Model.ComunicateService;
using pBot.Model.Core;
using pBot.Model.Core.Cache;
using pBot.Model.Core.Data;
using pBot.Model.Functions.HighLevel;

namespace pBotTests.HighLevel
{
    [TestFixture]
    public class BaseRepeaterTests
    {
        [SetUp]
        public void Setup()
        {
            repeater = new Repeater
            {
                CachedResponse = new CachedResponse<Command, string>(),
                CommandInvoker = new CommandInvoker(),
                Xmpp = Substitute.For<IXmpp>()
            };
        }

        private RepeaterBase repeater;

        private static readonly Command MockCommand = new Command("Test", "Test", Command.CommandType.Default, "Test");

        private static Command ItReturnsMockCommand()
        {
            return MockCommand;
        }

        private readonly Command RepeatingCommand = new Command("Someone", "Auto", Command.CommandType.Default, "5",
            "Test", "Test", "Test"); // Inside is MockCommand

        private readonly Command AnotherRepeatingCommand = new Command("Someone", "Auto", Command.CommandType.Default,
            "5", "Test", "Test", "NotTest"); // Inside is MockCommand

        [Test]
        public void CheckRepeatingTasksLineCountFor2Deals()
        {
            var expectedLines = 6; //2 for help, 2 per "Deal"
            repeater.DealWithRepeating(RepeatingCommand);
            repeater.DealWithRepeating(AnotherRepeatingCommand);

            var countOfLines = repeater.GetCurrentTasks().Split('\n').Length;

            Assert.AreEqual(expectedLines, countOfLines, repeater.GetCurrentTasks());
        }

        [Test]
        public void CheckRepeatingTasksLineCountForOneDeal()
        {
            var expectedLines = 4; //2 for help, 2 per "Deal"

            repeater.DealWithRepeating(RepeatingCommand);
            var countOfLines = repeater.GetCurrentTasks().Split('\n').Length;

            Assert.AreEqual(expectedLines, countOfLines, repeater.GetCurrentTasks());
        }

        [Test]
        public void ThrowsExceptionWhenCommandIsntHighLevel()
        {
            Assert.Throws<ArgumentException>(
                () => { repeater.DealWithRepeating(MockCommand); });
        }
    }
}
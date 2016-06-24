using System;
using NSubstitute;
using NUnit.Framework;
using pBot.Model.Commands.Helpers;
using pBot.Model.Commands.HighLevel;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBotTests.Model.Commands.HighLevel
{
    [TestFixture]
    public class SubscriptionTests
    {
        [SetUp]
        public void SetUp()
        {
            mockXmpp = Substitute.For<IXmpp>();
            mockXmpp.PrivateSend(string.Empty, SuccessMessage);

            subscription = new Subscription();

            var invoker = Substitute.For<ICommandInvoker>();
            invoker.InvokeCommand(EmbeedCommand).Returns(SuccessMessage);

            subscription.CommandInvoker = invoker;
            subscription.Xmpp = mockXmpp;
            subscription.CachedResponse = Substitute.For<CachedResponse>();
        }

        private Subscription subscription;

        private readonly string SuccessMessage = nameof(SuccessMessage);


        private readonly Command MasterCommand = new Command(string.Empty, "Auto", Command.CommandType.Any, "5");
        private readonly Command EmbeedCommand = new Command(string.Empty, "Do", Command.CommandType.Any);

        private Command MergedCommand => MasterCommand.MergeCommands(EmbeedCommand);

        private IXmpp mockXmpp;

        [Test]
        public void CommandInvoking()
        {
            subscription.DealWithRepeating(MergedCommand);

            mockXmpp.Received().PrivateSend(string.Empty, SuccessMessage);
        }

        [Test]
        public void ThisTestShouldFail()
        {
            Assert.Throws<ArgumentException>(
                () => {
                          subscription.DealWithRepeating(EmbeedCommand); // It should fail, bcos subscription 
                });
        }
    }
}
using System;
using System.Threading.Tasks;
using Autofac;
using NSubstitute;
using NSubstitute.Extensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using pBot.Model.Commands.Helpers;
using pBot.Model.Commands.HighLevel;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBotTests.Model.Commands.HighLevel
{
    [TestFixture()]
    public class SubscriptionTests
    {
        private Subscription subscription;

        private string SuccessMessage = nameof(SuccessMessage);


        Command MasterCommand = new Command(String.Empty, "Auto",Command.CommandType.Any, "5");
        Command EmbeedCommand = new Command(String.Empty, "Do",Command.CommandType.Any);

        private Command MergedCommand => MasterCommand.MergeCommands(EmbeedCommand);

        private IXmpp mockXmpp;

        [SetUp]
        public void SetUp()
        {
            mockXmpp = Substitute.For<IXmpp>();
            mockXmpp.PrivateSend(String.Empty, SuccessMessage);

            subscription = new Subscription();

            var invoker = Substitute.For<ICommandInvoker>();
            invoker.InvokeCommand(EmbeedCommand).Returns(SuccessMessage);

            subscription.CommandInvoker = invoker;
            subscription.Xmpp = mockXmpp;
            subscription.CachedResponse = Substitute.For<CachedResponse>();
        }

        [Test]
        public void ThisTestShouldFail()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                subscription.DealWithRepeating(EmbeedCommand); // It should fail, bcos subscription 
            });
        }

        [Test]
        public void CommandInvoking()
        {

            subscription.DealWithRepeating(MergedCommand);

            mockXmpp.Received().PrivateSend(String.Empty, SuccessMessage);
        }
    }
}
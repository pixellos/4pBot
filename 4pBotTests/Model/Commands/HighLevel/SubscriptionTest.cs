using Autofac;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using pBot.Model.Commands.HighLevel;
using pBot.Model.ComunicateService;
using pBot.Model.Core;
using static pBotTests.Model.Commands.Marshaller.CommandMarshallerConst;

namespace pBotTests.Model.Commands.HighLevel
{
    [TestFixture]
    public class SubscriptionTest
    {
        Subscription subscription = new Subscription();

        [SetUp]
        public void SetUp()
        {
            subscription = new Subscription();
            var xmpp = Substitute.For<IXmpp>();
            subscription.Xmpp = new XmppFree();

            subscription.CachedResponse = pBot.Dependencies.AutofacSetup.GetContainer().BeginLifetimeScope().Resolve<CachedResponse>();
            subscription.CommandInvoker = Substitute.For<ICommandInvoker>();
        }

        [Test]
        public void TestFor2Users()
        {
            Command command1 = new Command("User1","DoSomething",Command.CommandType.Any,Command.Any);
            Command command2 = new Command("User1","DoSomething",Command.CommandType.Any,Command.Any);

            subscription.CommandSubscribe(command1);
            subscription.CommandSubscribe(command1);
            subscription.CommandSubscribe(command2);
        }
    }
}

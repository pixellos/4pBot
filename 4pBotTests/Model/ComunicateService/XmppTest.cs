using NSubstitute;
using NUnit.Framework;
using pBot.Model.Commands.HighLevel;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBotTests.Model.ComunicateService
{
    [TestFixture]
    public class XmppTest
    {
        [SetUp]
        public void SetUp()
        {
            xmpp = Substitute.For<IXmpp>();
            Invoker = Substitute.For<ICommandInvoker>();
            repeater = new Repeater
            {
                Xmpp = xmpp,
                CommandInvoker = Invoker,
                CachedResponse = new CachedResponse()
            };

            //AutofacSetup.GetContainer().Resolve<Xmpp>();
        }

        private IXmpp xmpp;
        private Repeater repeater = new Repeater();
        private ICommandInvoker Invoker;

        [Test]
        public void Test()
        {
            repeater.DealWithRepeating(new Command("test", "Auto", Command.CommandType.Any, "10", "Check", "SO", "C#"));
            repeater.DealWithRepeating(new Command("test", "Auto", Command.CommandType.Negation, "10", "Check", "SO",
                "C#"));
        }
    }
}
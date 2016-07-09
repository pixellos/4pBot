using System;
using System.Diagnostics.CodeAnalysis;
using CoreBot;
using CoreBot.Mask;
using NUnit.Framework;

namespace CoreBotTests.Order
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ActionTests
    {
        private Actions actions;
        private Mask mask;
        Func<Result, string> expectedAction = (x) => "ReturnString";

        [SetUp]
        public void Setup()
        {
            actions = new Actions();
            mask = Builder.Bot().Requried("Test").End();

        }

        [Test]
        public void IntegrationTest()
        {
            actions.Add(mask, expectedAction);
            Assert.AreEqual(actions.InvokeMatchingAction("", "Bot Test"),"ReturnString");
        }

        [Test]
        public void IndexerActionTest()
        {
            actions[mask] = expectedAction;

            Assert.AreEqual(actions[mask], expectedAction);
        }
    }
}
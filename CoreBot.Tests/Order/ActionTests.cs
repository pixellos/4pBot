using System;
using System.Diagnostics.CodeAnalysis;
using CoreBot.Mask;
using NUnit.Framework;

namespace CoreBot.Tests.Order
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ActionTests
    {
        private Actions actions;
        private Mask.Mask mask;
        Func<Result, string> expectedAction = (x) => "ReturnString";

        [SetUp]
        public void Setup()
        {
            this.actions = new Actions();
            this.mask = Builder.Bot().Requried("Test").End();

        }

        [Test]
        public void IntegrationTest()
        {
            this.actions.Add(this.mask, this.expectedAction);
            Assert.AreEqual(this.actions.InvokeMatchingAction("", Builder.Name + " Test"), "ReturnString");
        }

        [Test]
        public void IndexerActionTest()
        {
            this.actions[this.mask] = this.expectedAction;

            Assert.AreEqual(this.actions[this.mask], this.expectedAction);
        }
    }
}
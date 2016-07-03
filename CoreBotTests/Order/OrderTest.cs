using System;
using CoreBot;
using CoreBot.Mask;
using NUnit.Framework;

namespace CoreBotTests.Order
{
    [TestFixture]
    public class OrderTest
    {
        [Test]
        public void IntegrationTest()
        {
            Orderer orderer  = new Orderer();
            orderer.AddTemporaryCommand(Builder.Bot().Then("Test").End(),(x)=> "ReturnString");

            Assert.AreEqual(orderer.InvokeConnectedAction("", "Bot Test"),"ReturnString");
        }

        [Test]
        public void InvokingCommand()
        {
            var sampleMask = Builder.Bot().Then("Test").End();
            var orderer = new Orderer();
            bool isHitted = false;
            orderer.AddTemporaryCommand(sampleMask,x => {
                isHitted = true;
                return isHitted.ToString();
            });

            orderer.InvokeConnectedAction("", sampleMask.SampleInput);

            Assert.True(isHitted);
        }

        [Test]
        public void NotMatchingMask_ReturnsNull()
        {
            var sampleMask = Builder.Bot().Then("Test").End();
            var orderer = new Orderer();

            Assert.Null(orderer.InvokeConnectedAction("", sampleMask.SampleInput));
        }

        [Test]
        public void FewMasks_IsCorrectInvoked()
        {
            string sampleMaskText = nameof(sampleMaskText);
            var sampleMask = Builder.Bot().Then(sampleMaskText).End();

            string sampleMask2Text = nameof(sampleMask2Text);
            var sampleMask2 = Builder.Bot().Then(sampleMask2Text).End();

            var orderer = new Orderer();
            orderer.AddTemporaryCommand(sampleMask, x => sampleMaskText);
            orderer.AddTemporaryCommand(sampleMask2, x => sampleMask2Text);


            var response = orderer.InvokeConnectedAction("", sampleMask2.SampleInput);

            Assert.AreSame(
                sampleMask2Text,
                response);
        }

        [Test]
        public void IsHelpReturnGoodData()
        {
            var sampleMask = Builder.Prepare().Then("Test").End();
            var orderer = new Orderer();
            orderer.AddTemporaryCommand(sampleMask, x => String.Empty);

            Assert.NotNull(orderer.GetHelpAboutAllCommands());
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using CoreBot.Mask;
using NUnit.Framework;
using _4PBot.Model.Functions;

namespace CoreBot.Tests.Order
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class OrderTest
    {
        public void InvokingCommand()
        {
            var sampleMask = Builder.Bot().Requried("Test").End();
            var orderer = new Actions();
            bool isHitted = false;
            orderer.Add(sampleMask, x =>
            {
                isHitted = true;
                return isHitted.ToString();
            });
            orderer.InvokeMatchingAction("", sampleMask.SampleInput);
            Assert.True(isHitted);
        }

        [Test]
        public void NotMatchingMask_ReturnsNull()
        {
            var sampleMask = Builder.Bot().Requried("Test").End();
            var orderer = new Actions();
            Assert.Null(orderer.InvokeMatchingAction("", sampleMask.SampleInput));
        }

        [Test]
        public void FewMasks_IsCorrectInvoked()
        {
            string sampleMaskText = nameof(sampleMaskText);
            string sampleMask2Text = nameof(sampleMask2Text);
            var sampleMask = Builder.Bot().Requried(sampleMaskText).End();
            var sampleMask2 = Builder.Bot().Requried(sampleMask2Text).End();
            var orderer = new Actions
            {
                { sampleMask, x => sampleMaskText },
                { sampleMask2, x => sampleMask2Text }
            };
            var response = orderer.InvokeMatchingAction("", sampleMask2.SampleInput);
            Assert.AreSame(sampleMask2Text, response);
        }

        [Test]
        public void IsHelpReturnGoodData()
        {
            var sampleMask = Builder.Bot().Requried("Test").End();
            var orderer = new Actions
            {
                { sampleMask, x => String.Empty }
            };
            var help = new ActionsHelp(orderer);
            Assert.NotNull(help.BuildHelpString());
        }
    }
}

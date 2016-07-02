using BotOrder;
using NUnit.Framework;
using static BotOrder.Mask.Builder;

namespace pBotTests.Order
{
    [TestFixture]
    public class Doer
    {
        [Test]
        public void IntegrationTest()
        {
            Orderer orderer  = new Orderer();
            orderer.AddTemporaryCommand(Bot().Then("Test").FinalizeCommand(),(x)=> "ReturnString");

            Assert.AreEqual(orderer.InvokeCommand("", "Bot Test"),"ReturnString");
        }
    }
}

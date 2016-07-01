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
            OrderDoer orderDoer  = new OrderDoer();
            orderDoer.AddTemporaryCommand(Bot().ThenRequried("Test").FinalizeCommand(),(x)=> "ReturnString");

            Assert.AreEqual(orderDoer.InvokeCommand("", "Bot Test"),"ReturnString");
        }
    }
}

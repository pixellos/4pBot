using NUnit.Framework;
using pBot.Model.Order;
using pBot.Model.Order.Mask;

namespace pBotTests.Order
{
    [TestFixture]
    public class Doer
    {
        [Test]
        public void IntegrationTest()
        {
            OrderDoer orderDoer  = new OrderDoer();
            orderDoer.AddTemporaryCommand(Builder.Prepare().Bot().ThenRequried("Test").FinalizeCommand(),(x)=> "ReturnString");

            Assert.AreEqual(orderDoer.InvokeCommand("", "Bot Test"),"ReturnString");
        }
    }
}

using NUnit.Framework;
using pBot.Model.Core.Cache;
using pBotTests.Model.Commands.Marshaller;

namespace pBotTests.Model.Commands.Cache
{
    [TestFixture]
    public class CacheTest
    {
        [SetUp]
        public void SetUp()
        {
            cachedResponse = new CachedResponse();
        }

        private CachedResponse cachedResponse;


        [Test]
        public void CheckResponseUniquenes()
        {
            cachedResponse.SetLastResponse(CommandMarshallerConst.Show_Author_Command, "");
            cachedResponse.SetLastResponse(CommandMarshallerConst.Show_Author_Command, "4334331433");

            Assert.True(cachedResponse.IsResponseUnique(CommandMarshallerConst.Show_Author_Command,
                "jreoiueoiujfsdoiue09"));
        }

        [Test]
        public void IsExistAtCache()
        {
            cachedResponse.SetLastResponse(CommandMarshallerConst.Show_Author_Command, "");
            cachedResponse.SetLastResponse(CommandMarshallerConst.Show_Author_Command, "Test");

            var expectedNull = "";
            cachedResponse.DoWhenResponseIsNotLikeLastResponse(CommandMarshallerConst.Show_Author_Command, null, x =>
            {
                expectedNull = x;
                Assert.Pass();
            });

            Assert.IsNull(expectedNull);
        }

        [Test]
        public void RemoveFromCache()
        {
            cachedResponse.SetLastResponse(CommandMarshallerConst.Show_Author_Command, "");
            cachedResponse.Remove(CommandMarshallerConst.Show_Author_Command);
            Assert.False(cachedResponse.ContainsCommand(CommandMarshallerConst.Show_Author_Command));
        }
    }
}
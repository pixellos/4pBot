using NUnit.Framework;
using pBot.Model.DataStructures;

namespace _4pBotTests.Model.DataStructures
{
    [TestFixture()]
    public class CachedResponseTests
    {
        CachedResponse<string,string> cachedResponse = new CachedResponse<string, string>();


        private string firstKey = "FirstKey";
        private string firstValue = "FirstVal";
        [SetUp]
        public void SetUp()
        {
            cachedResponse = new CachedResponse<string, string>();
            cachedResponse.InitializeKey(firstKey,firstValue);
        }

        [Test()]
        public void ContainsKeyTest()
        {
            Assert.IsTrue(cachedResponse.ContainsKey(firstKey));
        }

        [Test()]
        public void IsResponseUniqueTest()
        {
            Assert.IsTrue(cachedResponse.IsResponseUnique(firstKey,"Not firstValue value"));
        }

        [Test()]
        public void SetLastResponseTest()
        {
            string newValue = nameof(newValue);
            cachedResponse.SetLastResponse(firstKey,newValue);

            Assert.AreEqual(newValue,cachedResponse.GetCacheValue(firstKey));
        }

        [Test()]
        public void DoWhenResponseIsNotLikeLastResponseTest()
        {
            cachedResponse.DoWhenResponseIsNotLikeLastResponse(firstKey,"NotLastLike",s => Assert.AreEqual(s,"NotLastLike") );
        }

        [Test()]
        public void GetCacheValueTest()
        {
            Assert.AreEqual(firstValue,cachedResponse.GetCacheValue(firstKey));
        }

        [Test()]
        public void InitializeKeyTest()
        {
            string key = "key";
            string val = "val";

            cachedResponse.InitializeKey(key,val);

            Assert.AreEqual(val, cachedResponse.GetCacheValue(key));
        }

        [Test()]
        public void RemoveTest()
        {
            cachedResponse.Remove(firstKey);

            Assert.IsTrue(null == cachedResponse.GetCacheValue(firstKey));
        }
    }
}
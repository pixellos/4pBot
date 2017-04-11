using NUnit.Framework;
using _4PBot.Model.DataStructures;

namespace _4pBot.Tests.Model.DataStructures
{
    [TestFixture()]
    public class CachedResponseTests
    {
        CachedResponse<string, string> cachedResponse = new CachedResponse<string, string>();
        private string firstKey = "FirstKey";
        private string firstValue = "FirstVal";

        [SetUp]
        public void SetUp()
        {
            this.cachedResponse = new CachedResponse<string, string>();
            this.cachedResponse.InitializeKey(this.firstKey, this.firstValue);
        }

        [Test()]
        public void ContainsKeyTest()
        {
            Assert.IsTrue(this.cachedResponse.ContainsKey(this.firstKey));
        }

        [Test()]
        public void IsResponseUniqueTest()
        {
            Assert.IsTrue(this.cachedResponse.IsResponseUnique(this.firstKey, "Not firstValue value"));
        }

        [Test()]
        public void SetLastResponseTest()
        {
            string newValue = nameof(newValue);
            this.cachedResponse.SetLastResponse(this.firstKey, newValue);

            Assert.AreEqual(newValue, this.cachedResponse.GetCacheValue(this.firstKey));
        }

        [Test()]
        public void DoWhenResponseIsNotLikeLastResponseTest()
        {
            this.cachedResponse.DoWhenResponseIsNotLikeLastResponse(this.firstKey, "NotLastLike", s => Assert.AreEqual(s, "NotLastLike"));
        }

        [Test()]
        public void GetCacheValueTest()
        {
            Assert.AreEqual(this.firstValue, this.cachedResponse.GetCacheValue(this.firstKey));
        }

        [Test()]
        public void InitializeKeyTest()
        {
            string key = "key";
            string val = "val";

            this.cachedResponse.InitializeKey(key, val);

            Assert.AreEqual(val, this.cachedResponse.GetCacheValue(key));
        }

        [Test()]
        public void RemoveTest()
        {
            this.cachedResponse.Remove(this.firstKey);

            Assert.IsTrue(null == this.cachedResponse.GetCacheValue(this.firstKey));
        }
    }
}
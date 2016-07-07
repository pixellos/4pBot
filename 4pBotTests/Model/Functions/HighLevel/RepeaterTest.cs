using System;
using NUnit.Framework;
using pBot.Model.DataStructures;
using pBot.Model.Functions.HighLevel;

namespace _4pBotTests.Model.Functions.HighLevel
{
    [TestFixture()]
    public class RepeaterTest
    {
        [Test()]
        public void CheckRequestsTest()
        {
            Repeater repeater = new Repeater()
            {
                CachedResponse = new CachedResponse<string, string>(),
                SendCommand = s => { }
            };

            Assert.IsTrue(repeater.CheckRequests().Equals(String.Empty));
        }


        [Test()]
        public void CheckRequestsNotNullTest()
        {
            Repeater repeater = new Repeater()
            {
                CachedResponse = new CachedResponse<string, string>(),
                SendCommand = s => { }
            };

            repeater.Add(5,"Test", () => "");

            Assert.IsFalse(repeater.CheckRequests().Equals(String.Empty));
        }

        [Test()]
        public void AddRequestTest()
        {
            var cachedResponse = new CachedResponse<string, string>();
            Repeater repeater = new Repeater()
            {
                CachedResponse = cachedResponse,
                SendCommand = s => { }
            };

            repeater.Add(5,"Test", () => "");
            
            Assert.IsTrue(cachedResponse.ContainsKey("Test"));
        }

        [Test()]
        public void RemoveRequestTest()
        {
            var cachedResponse = new CachedResponse<string, string>();
            Repeater repeater = new Repeater()
            {
                CachedResponse = cachedResponse,
                SendCommand = s => { }
            };

            repeater.Add(5,"Test", () => "");
            repeater.RemoveRequest("Test");

            Assert.IsFalse(cachedResponse.ContainsKey("Test"));
        }
    }
} 
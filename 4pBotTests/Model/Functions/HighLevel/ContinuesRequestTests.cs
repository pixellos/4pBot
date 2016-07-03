using NUnit.Framework;
using pBot.Model.Functions.HighLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pBot.Model.Functions.HighLevel.Tests
{
    [TestFixture()]
    public class ContinuesRequestTests
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

            repeater.AddRequest("Test", () => "");

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

            repeater.AddRequest("Test", () => "");
            
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

            repeater.AddRequest("Test", () => "");
            repeater.RemoveRequest("Test");

            Assert.IsFalse(cachedResponse.ContainsKey("Test"));
        }
    }
}
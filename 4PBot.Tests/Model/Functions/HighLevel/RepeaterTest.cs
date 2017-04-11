using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using _4PBot.Model.DataStructures;
using _4PBot.Model.Functions.HighLevel;

namespace _4pBot.Tests.Model.Functions.HighLevel
{
    [ExcludeFromCodeCoverage]
    [TestFixture()]
    public class RepeaterTest
    {
        [Test()]
        public void CheckRequestsTest()
        {
            Repeater repeater = new Repeater()
            {
                CachedResponse = new CachedResponse<string, string>(),
            };

            Assert.IsTrue(repeater.CheckRequests().Equals(String.Empty));
        }


        [Test()]
        public void CheckRequestsNotNullTest()
        {
            Repeater repeater = new Repeater()
            {
                CachedResponse = new CachedResponse<string, string>(),
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
            };

            repeater.Add(5,"Test", () => "");
            repeater.RemoveRequest("Test");

            Assert.IsFalse(cachedResponse.ContainsKey("Test"));
        }
    }
} 
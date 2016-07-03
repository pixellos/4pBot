using System;
using System.Net;
using NUnit.Framework;

using pBot.Model.Helper;

namespace _4pBotTests.Model.Functions._4pChecker
{
    [TestFixture()]
    public class UrlShortenerTests
    {
        [Test()]
        public void GetShortUrlTest()
        {
            string testAdress = @"http://nunit.org";
            var shortUrl = testAdress.GetShortUrl();

            var firstWeb = new WebClient().DownloadString(new Uri(shortUrl));
            var secondWeb = new WebClient().DownloadString(new Uri(testAdress));

            Assert.AreEqual(firstWeb,secondWeb);
        }
    }
}
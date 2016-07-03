using System;
using NSubstitute;
using NUnit.Framework;
using pBot.Model.Functions.Checkers._4pChecker;
using _4pBotTests.Properties;

namespace _4pBotTests.Model.Functions._4pChecker
{
    [TestFixture()]
    public class Checker4PTests
    {

        [Test]
        public void NoMatchingForum()
        {
            Checker4P checker4P = new Checker4P();
            var response = checker4P.GetLastPostAtCategory("3iu32o5u9uj");

            Assert.AreEqual(Checker4P.NoMatchingForumMeessage,response);
        }

        [Test()]
        public void GetNewestPostTest()
        {
            var downloader = new Downloader4P();

            var JsonDeserialized = downloader.Deserialize(Resources._4pJson);

            Downloader4P Downloader4P = Substitute.For<Downloader4P>();
            Downloader4P.WhenForAnyArgs(x=>x.DownloadData("x")).DoNotCallBase();
            Downloader4P.DownloadData("x").ReturnsForAnyArgs(JsonDeserialized);
            Checker4P checker4P = new Checker4P();
            checker4P.Downloader4P = Downloader4P;

            Console.WriteLine(checker4P.GetLastPostAtCategory("Java"));
            Assert.IsTrue(checker4P.GetLastPostAtCategory("Java").StartsWith("Java: Rozszerzenie listy jednokierunkowej, przez Stang: "));
        }
    }
}
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
            Checker checker4P = new Checker();
            var response = checker4P.GetLastPostAtCategory("3iu32o5u9uj");

            Assert.AreEqual(Checker.NoMatchingForumMeessage,response);
        }

        [Test()]
        public void GetNewestPostTest()
        {
            var downloader = new Downloader4P();

            var JsonDeserialized = downloader.Deserialize(Resources._4pJson);

            Downloader4P Downloader4P = Substitute.For<Downloader4P>();
            Downloader4P.WhenForAnyArgs(x=>x.DownloadData("x","x")).DoNotCallBase();
            Downloader4P.DownloadData("x","x").ReturnsForAnyArgs(JsonDeserialized);
            Checker checker4P = new Checker();
            checker4P.Downloader4P = Downloader4P;

            Console.WriteLine(checker4P.GetLastPostAtCategory("Java"));
            Assert.IsTrue(checker4P.GetLastPostAtCategory("Java").StartsWith("Java: Rozszerzenie listy jednokierunkowej, przez Stang: "));
        }
    }
}
using System;
using NSubstitute;
using NUnit.Framework;
using _4PBot.Model.Functions._4Programmers;
using _4pBot.Tests.Properties;

namespace _4pBot.Tests.Model.Functions._4Programmers
{
    [TestFixture()]
    public class CheckerTests
    {
        [Test]
        public void NoMatchingForum()
        {
            var checker = new Checker();
            var response = checker.GetLastPostAtCategory("3iu32o5u9uj");
            Assert.AreEqual(Checker.NoMatchingForumMeessage, response);
        }

        [Test()]
        public void GetNewestPostTest()
        {
            var downloader = new Downloader();
            var deserializer = downloader.Deserialize(Resources._4pJson);
            Downloader Downloader4P = Substitute.For<Downloader>();
            Downloader4P.WhenForAnyArgs(x => x.DownloadData("x", "x")).DoNotCallBase();
            Downloader4P.DownloadData("x", "x").ReturnsForAnyArgs(deserializer);
            var checker4P = new Checker();
            checker4P.Downloader4P = Downloader4P;

            Console.WriteLine(checker4P.GetLastPostAtCategory("Java"));
            Assert.IsTrue(checker4P.GetLastPostAtCategory("Java").StartsWith("Java: Rozszerzenie listy jednokierunkowej, przez Stang: "));
        }
    }
}
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
            var d = new TopicsContiniousDownloading("Test");
            var checker = new Checker(d);
            var response = checker.GetLastPostAtCategory("3iu32o5u9uj");
            Assert.AreEqual(Checker.NoMatchingForumMeessage, response);
        }

        [Test()]
        public void GetNewestPostTest()
        {
            var downloader = Substitute.For<TopicsContiniousDownloading>();
            var deserializer = downloader.Deserialize(Resources._4pJson);
            var checker4P = new Checker(downloader);
            downloader.WhenForAnyArgs(x => x.DownloadData("x")).DoNotCallBase();
            downloader.DownloadData("x").ReturnsForAnyArgs(deserializer.Posts);
            Console.WriteLine(checker4P.GetLastPostAtCategory("Java"));
            Assert.IsTrue(checker4P.GetLastPostAtCategory("Java").StartsWith("Java: Rozszerzenie listy jednokierunkowej, przez Stang: "));
        }
    }
}
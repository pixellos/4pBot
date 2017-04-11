using NSubstitute;
using NUnit.Framework;
using pBot.Model.Functions.Checkers.SOChecker;
using _4pBotTests.Properties;

namespace _4pBotTests.Model.Functions.Checkers.SOChecker
{
    [TestFixture()]
    public class CheckerSOTests
    {
        [Test()]
        public void CheckNewestByTagTest()
        {
            Checker checkerSo = new Checker();

            Downloader downloaderSo = NSubstitute.Substitute.For<Downloader>();
            downloaderSo.WhenForAnyArgs(x=>x.GetWebString("")).DoNotCallBase();
            downloaderSo.GetWebString("").ReturnsForAnyArgs(Resources.SOHtml);

            checkerSo.DownloaderSo = downloaderSo;

            var response = checkerSo.CheckNewestByTag("Java");
            
            

            Assert.AreEqual(response, "Java: How to guarantee that equals() and hashCode() are in sync? http://goo.gl/AD98RK");
        }


        [Test()]
        public void NoMatchingTest()
        {
            Checker checkerSo = new Checker()
            {
                DownloaderSo = new Downloader()
            };

            var response = checkerSo.CheckNewestByTag("I'm pretty sure, that you'll never found this :)");
            Assert.AreEqual(response, Checker.CantFindRequestMessage);
        }
    }
}
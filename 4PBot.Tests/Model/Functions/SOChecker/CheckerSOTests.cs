using _4pBot.Tests.Properties;
using _4PBot.Model.Functions.StackOverflow;
using NSubstitute;
using NUnit.Framework;

namespace _4pBot.Tests.Model.Functions.SOChecker
{
    [TestFixture()]
    public class CheckerSOTests
    {
        [Test()]
        public void CheckNewestByTagTest()
        {
            var downloader = NSubstitute.Substitute.For<Downloader>();
            var checkerSo = new Checker(downloader);
            downloader.WhenForAnyArgs(x => x.GetWebString("")).DoNotCallBase();
            downloader.GetWebString("").ReturnsForAnyArgs(Resources.SOHtml);
            var response = checkerSo.CheckNewestByTag("Java");
            Assert.AreEqual(response, "Java: How to guarantee that equals() and hashCode() are in sync? http://goo.gl/AD98RK");
        }

        [Test()]
        public void NoMatchingTest()
        {
            var checkerSo = new Checker(new Downloader());
            var response = checkerSo.CheckNewestByTag("I'm pretty sure, that you'll never found this :)");
            Assert.AreEqual(response, Checker.CantFindRequestMessage);
        }
    }
}
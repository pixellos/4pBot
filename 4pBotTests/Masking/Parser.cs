using NUnit.Framework;
using NUnit.Common;
using NUnit.Framework.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pBot.Model.Order.Mask;
using static pBot.Model.Order.Mask.Builder;

namespace pBotTests.Model.Commands.MaskTests
{
    [TestFixture]
    public class Parser
    {
        public static IEnumerable MatchTestData
        {
            get
            {
                yield return new TestCaseData(
                    Prepare().Bot().FinalizeCommand(), "Bot ").
                    Returns(new Dictionary<string,string>()
                {
                    ["Bot"] = ""
                });

                yield return new TestCaseData(Prepare().Bot().ThenWord("SomeWord", "Example").FinalizeCommand(),"Bot test ").Returns(new Dictionary<string,string>()
                {
                    ["Bot"] = "",
                    ["SomeWord"] = "test"
                });

                yield return new TestCaseData(Prepare().Bot().ThenNonWhiteSpaceString("Q","1234").FinalizeCommand(), "Bot 8796381147869adsa").Returns(new Dictionary<string, string>()
                {
                    ["Bot"] = "",
                    ["Q"] = "8796381147869adsa"
                });

             

                yield return new TestCaseData(Prepare().Bot().ThenWord("SomeWord", "Example").ThenEverythingToEndOfLine("Everything", "some input").FinalizeCommand(), "Bot test trata rata").Returns(new Dictionary<string, string>()
                {
                    ["Bot"] = "",
                    ["SomeWord"] = "test",
                    ["Everything"] = "trata rata"
                });
            }
        }

        
        [Test,TestCaseSource(nameof(MatchTestData))]
        public Dictionary<string,string> MatchTest(Mask mask, string TextToParse)
        {
            Result afterParse = null;

            Assert.DoesNotThrow(() =>
            {
                afterParse = mask.Parse("", TextToParse);
            });

            return afterParse.MatchedResult;
        }

        [Test]
        public void MatchTest_Throw_FormatException_When_StringHasIncorrectFormat()
        {
            var mask = Prepare().Bot().FinalizeCommand();
            var incorrectInput = "Bo312ts some chat message :D :P#$@#$23k43io2j44";

            Assert.Throws<FormatException>(() => mask.Parse("TES", incorrectInput));
        }
    }
}

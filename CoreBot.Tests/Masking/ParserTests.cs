using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CoreBot.Mask;
using NUnit.Framework;

namespace CoreBot.Tests.Masking
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ParserTests
    {
        public static IEnumerable MatchTestData
        {
            get
            {
                yield return new TestCaseData(
                    Builder.Bot().End(), "Bot ").
                    Returns(new Dictionary<string, string>()
                    {
                        ["Bot"] = ""
                    });
                yield return new TestCaseData(
                Builder.Bot().ThenEverythingToEndOfLine("All").End(), "Bot 12345 qwerty !@#$%^&").
                    Returns(new Dictionary<string, string>()
                    {
                        ["Bot"] = "",
                        ["All"] = "12345 qwerty !@#$%^&"
                    });
                yield return new TestCaseData(Builder.Bot().ThenWord("SomeWord", "Example").End(), "Bot test ").
                    Returns(new Dictionary<string, string>()
                    {
                        ["Bot"] = "",
                        ["SomeWord"] = "test"
                    });
                yield return new TestCaseData(Builder.Bot().ThenString("Q", "1234").End(), "Bot 8796381147869adsa").
                    Returns(new Dictionary<string, string>()
                    {
                        ["Bot"] = "",
                        ["Q"] = "8796381147869adsa"
                    });
                yield return new TestCaseData(Builder.Bot().ThenWord("SomeWord", "Example").ThenEverythingToEndOfLine("Everything", "some input").End(), "Bot test trata rata").
                    Returns(new Dictionary<string, string>()
                    {
                        ["Bot"] = "",
                        ["SomeWord"] = "test",
                        ["Everything"] = "trata rata"
                    });
            }
        }

        [Test, TestCaseSource(nameof(ParserTests.MatchTestData))]
        public Dictionary<string, string> MatchTest(Mask.Mask mask, string TextToParse)
        {
            var result = (SuccededResult)mask.Parse("", TextToParse);
            return result.MatchedResult;
        }

        [Test, TestCaseSource(nameof(ParserTests.MatchTestData))]
        public Dictionary<string, string> MatchTestByIndexer(Mask.Mask mask, string TextToParse)
        {
            var afterParse = (SuccededResult)mask.Parse("", TextToParse);
            foreach (KeyValuePair<string, string> keyValuePair in afterParse.MatchedResult)
            {
                Assert.AreEqual(keyValuePair.Value, afterParse[keyValuePair.Key]);
            }
            return afterParse.MatchedResult;
        }

        [Test]
        public void MatchTest_Throw_FormatException_When_StringHasIncorrectFormat()
        {
            var mask = Builder.Bot().End();
            var incorrectInput = "Bo312ts some chat message :D :P#$@#$23k43io2j44";
            var result = mask.Parse("TES", incorrectInput);
            Assert.IsNotInstanceOf<SuccededResult>(result);
        }
    }
}

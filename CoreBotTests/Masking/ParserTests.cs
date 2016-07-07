﻿using System;
using System.Collections;
using System.Collections.Generic;
using CoreBot.Mask;
using NUnit.Framework;

namespace CoreBotTests.Masking
{
    [TestFixture]
    public class ParserTests
    {
        public static IEnumerable MatchTestData
        {
            get
            {
                yield return new TestCaseData(
                    Builder.Bot().End(), "Bot ").
                    Returns(new Dictionary<string,string>()
                {
                    ["Bot"] = ""
                });

                yield return new TestCaseData(Builder.Bot().ThenWord("SomeWord", "Example").End(),"Bot test ").Returns(new Dictionary<string,string>()
                {
                    ["Bot"] = "",
                    ["SomeWord"] = "test"
                });

                yield return new TestCaseData(Builder.Bot().ThenString("Q","1234").End(), "Bot 8796381147869adsa").Returns(new Dictionary<string, string>()
                {
                    ["Bot"] = "",
                    ["Q"] = "8796381147869adsa"
                });

                yield return new TestCaseData(Builder.Bot().ThenWord("SomeWord", "Example").ThenEverythingToEndOfLine("Everything", "some input").End(), "Bot test trata rata").Returns(new Dictionary<string, string>()
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
            var mask = Builder.Bot().End();
            var incorrectInput = "Bo312ts some chat message :D :P#$@#$23k43io2j44";

            Assert.Throws<FormatException>(() => mask.Parse("TES", incorrectInput));
        }
    }
}

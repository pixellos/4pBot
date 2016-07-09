using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CoreBot.Mask;
using NUnit.Framework;

namespace CoreBotTests.Masking
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class MaskTests
    {
        private const string Code = "Code";
        private const string Something = "Something";
        const string SampleInput = "Input";

        public static IEnumerable SampleInputMatchRegexString
        {
            get
            {
                yield return new TestCaseData(Builder.Bot().End());
                yield return new TestCaseData(Builder.StartsWith("test").End());
                yield return new TestCaseData(Builder.Bot().ThenWord(Something,SampleInput).End());
                yield return new TestCaseData(Builder.Bot().ThenWord(Something,SampleInput).End());
                yield return new TestCaseData(Builder.Bot().ThenWord(Something,SampleInput).Requried(SampleInput).End());
                yield return new TestCaseData(Builder.Bot().ThenWord(Something,SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").End());
            }
        }

        [Test,TestCaseSource(nameof(SampleInputMatchRegexString))]
        public void IsSampleInputMatchingWithRegex(Mask mask)
        {
            Regex regex = new Regex(mask.RegexString);
            var result = regex.Match(mask.SampleInput);

            foreach (Group @group in result.Groups)
            {
                Console.WriteLine(@group.Value);
            }

            Assert.True(result.Success);
        }

        public static IEnumerable SampleInputCorrectMatch
        {
            get
            {
                yield return new TestCaseData(Builder.Bot().ThenWord(Something, SampleInput).End(),Something,SampleInput);
                yield return new TestCaseData(Builder.StartsWith("Test").ThenWord(Something, SampleInput).End(),Something,SampleInput);
                yield return new TestCaseData(Builder.Bot().ThenWord(Something, SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").End(),Code, @"var x = new x();");
                yield return new TestCaseData(Builder.Bot().ThenWord(Something, SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").End(),"Something","Input");
            }
        }
        [Test,TestCaseSource(nameof(SampleInputCorrectMatch))]
        public void IsMatchedSampleIsCorrect(Mask mask,string group, string estimatedMatch)
        {
            Regex regex = new Regex(mask.RegexString);
            var result = regex.Match(mask.SampleInput);

            var currentMatchValue = result.Groups[group].Value;

            Assert.AreEqual(estimatedMatch,currentMatchValue);
        }
    }
}

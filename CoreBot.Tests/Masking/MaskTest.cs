using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CoreBot.Mask;
using NUnit.Framework;

namespace CoreBot.Tests.Masking
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
                yield return new TestCaseData(Builder.Bot().ThenWord(MaskTests.Something,MaskTests.SampleInput).End());
                yield return new TestCaseData(Builder.Bot().ThenWord(MaskTests.Something,MaskTests.SampleInput).End());
                yield return new TestCaseData(Builder.Bot().ThenWord(MaskTests.Something,MaskTests.SampleInput).Requried(MaskTests.SampleInput).End());
                yield return new TestCaseData(Builder.Bot().ThenWord(MaskTests.Something,MaskTests.SampleInput).ThenEverythingToEndOfLine(MaskTests.Code, @"var x = new x();").End());
            }
        }

        [Test,TestCaseSource(nameof(MaskTests.SampleInputMatchRegexString))]
        public void IsSampleInputMatchingWithRegex(Mask.Mask mask)
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
                yield return new TestCaseData(Builder.Bot().ThenWord(MaskTests.Something, MaskTests.SampleInput).End(),MaskTests.Something,MaskTests.SampleInput);
                yield return new TestCaseData(Builder.StartsWith("Test").ThenWord(MaskTests.Something, MaskTests.SampleInput).End(),MaskTests.Something,MaskTests.SampleInput);
                yield return new TestCaseData(Builder.Bot().ThenWord(MaskTests.Something, MaskTests.SampleInput).ThenEverythingToEndOfLine(MaskTests.Code, @"var x = new x();").End(),MaskTests.Code, @"var x = new x();");
                yield return new TestCaseData(Builder.Bot().ThenWord(MaskTests.Something, MaskTests.SampleInput).ThenEverythingToEndOfLine(MaskTests.Code, @"var x = new x();").End(),"Something","Input");
            }
        }
        [Test,TestCaseSource(nameof(MaskTests.SampleInputCorrectMatch))]
        public void IsMatchedSampleIsCorrect(Mask.Mask mask,string group, string estimatedMatch)
        {
            Regex regex = new Regex(mask.RegexString);
            var result = regex.Match(mask.SampleInput);

            var currentMatchValue = result.Groups[group].Value;

            Assert.AreEqual(estimatedMatch,currentMatchValue);
        }
    }
}

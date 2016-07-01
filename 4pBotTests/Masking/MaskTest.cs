using System;
using System.Collections;
using System.Text.RegularExpressions;
using BotOrder.Mask;
using NUnit.Framework;
using static BotOrder.Mask.Builder;
namespace pBotTests.Masking
{
    [TestFixture]
    public class MaskTest
    {
        private const string Code = "Code";
        private const string Something = "Something";
        const string SampleInput = "Input";

        public static IEnumerable SampleInputMatchRegexString
        {
            get
            {
                yield return new TestCaseData(Bot().FinalizeCommand());
                yield return new TestCaseData(Bot().ThenWord(Something,SampleInput).FinalizeCommand());
                yield return new TestCaseData(Bot().ThenWord(Something,SampleInput).FinalizeCommand());
                yield return new TestCaseData(Bot().ThenWord(Something,SampleInput).ThenRequried(SampleInput).FinalizeCommand());
                yield return new TestCaseData(Bot().ThenWord(Something,SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand());
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
                yield return new TestCaseData(Bot().ThenWord(Something, SampleInput).FinalizeCommand(),Something,SampleInput);
                yield return new TestCaseData(Bot().ThenWord(Something, SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand(),Code, @"var x = new x();");
                yield return new TestCaseData(Bot().ThenWord(Something, SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand(),"Something","Input");
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

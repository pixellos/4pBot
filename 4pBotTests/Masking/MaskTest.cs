using System;
using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;
using pBot.Model.Order.Mask;

namespace pBotTests.Model.Commands.Masking
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
                yield return new TestCaseData(Builder.Prepare().Bot().FinalizeCommand());
                yield return new TestCaseData(Builder.Prepare().Bot().Bot().FinalizeCommand());
                yield return new TestCaseData(Builder.Prepare().Bot().ThenWord(Something,SampleInput).FinalizeCommand());
                yield return new TestCaseData(Builder.Prepare().ThenWord(Something,SampleInput).FinalizeCommand());
                yield return new TestCaseData(Builder.Prepare().ThenWord(Something,SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand());
            }
        }

        [Test,TestCaseSource(nameof(SampleInputMatchRegexString))]
        public void IsSampleInputMatchingWithRegex(pBot.Model.Order.Mask.Mask mask)
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
                yield return new TestCaseData(Builder.Prepare().Bot().ThenWord(Something, SampleInput).FinalizeCommand(),Something,SampleInput);
                yield return new TestCaseData(Builder.Prepare().ThenWord(Something, SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand(),Code, @"var x = new x();");
                yield return new TestCaseData(Builder.Prepare().ThenWord(Something, SampleInput).ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand(),"Something","Input");
            }
        }
        [Test,TestCaseSource(nameof(SampleInputCorrectMatch))]
        public void IsMatchedSampleIsCorrect(pBot.Model.Order.Mask.Mask mask,string group, string estimatedMatch)
        {
            Regex regex = new Regex(mask.RegexString);
            var result = regex.Match(mask.SampleInput);

            var currentMatchValue = result.Groups[group].Value;

            Assert.AreEqual(estimatedMatch,currentMatchValue);
        }
    }
}

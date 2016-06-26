using System;
using System.Collections;
using NUnit.Framework;
using System.Text.RegularExpressions;
using pBot.Model.Commands.Parser;

namespace pBotTests.Model.Commands.Parser
{
    [TestFixture]
    public class AdvancedParser
    {
        private const string Code = "Code";
        private const string Something = "Something";

        public static IEnumerable SampleInputMatchRegexString
        {
            get
            {
                yield return new TestCaseData(CommandBuilder.Prepare().Bot().FinalizeCommand());
                yield return new TestCaseData(CommandBuilder.Prepare().Bot().Bot().FinalizeCommand());
                yield return new TestCaseData(CommandBuilder.Prepare().Bot().ThenWord(Something,"Input").FinalizeCommand());
                yield return new TestCaseData(CommandBuilder.Prepare().ThenWord(Something,"Input").FinalizeCommand());
                yield return new TestCaseData(CommandBuilder.Prepare().ThenWord(Something,"Input").ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand());
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
                yield return new TestCaseData(CommandBuilder.Prepare().Bot().ThenWord(Something, "Input").FinalizeCommand(),Something,"Input");
                yield return new TestCaseData(CommandBuilder.Prepare().ThenWord(Something, "Input").ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand(),Code, @"var x = new x();");
                yield return new TestCaseData(CommandBuilder.Prepare().ThenWord(Something, "Input").ThenEverythingToEndOfLine(Code, @"var x = new x();").FinalizeCommand(),"Something","Input");
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

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using pBot.Model.Commands.Parser;
using CommandBuilder = pBot.Model.Commands.Parser.CommandBuilder;

namespace pBotTests.Model.Commands.Command
{
    [TestFixture]
    public class Builder
    {

        public static IEnumerable DataSource
        {
            get
            {
                yield return new TestCaseData(CommandBuilder.Prepare().Bot().FinalizeCommand());
            }
        }

        [Test,TestCaseSource(nameof(DataSource))]
        public void TestMethod(Mask mask)
        {
            Regex regex =  new Regex(mask.RegexString);
            Assert.True(regex.Match(mask.SampleInput).Success);
        }
    }
}

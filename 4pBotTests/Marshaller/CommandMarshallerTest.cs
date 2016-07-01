using System;
using System.Collections;
using Autofac;
using NUnit.Framework;
using pBot.Dependencies;
using pBot.Model.Core.Abstract;
using pBot.Model.Core.Data;
using pBot.Model.Functions.StackOverflowChecker;

namespace pBotTests.Marshaller
{
    [TestFixture]
    public class CommandMarshallerTest
    {
        private readonly ICommandParser parser;

        public CommandMarshallerTest()
        {
            var container = AutofacSetup.GetContainer();

            parser = container.Resolve<ICommandParser>();
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return
                    new TestCaseData(CommandMarshallerConst.Show_Author).Returns(
                        CommandMarshallerConst.Show_Author_Command);
                yield return
                    new TestCaseData(CommandMarshallerConst.Show_Time).Returns(CommandMarshallerConst.Show_Time_Command)
                    ;
                yield return
                    new TestCaseData(CommandMarshallerConst.Not_Bot_Call).Returns(
                        CommandMarshallerConst.Not_Bot_Call_Command);
                yield return
                    new TestCaseData(CommandMarshallerConst.Set_AutoCheck_SO_CSharp_True).Returns(
                        CommandMarshallerConst.Set_AutoCheck_SO_CSharp_True_Command);
                yield return
                    new TestCaseData(CommandMarshallerConst.Set_AutoCheck_SO_CSharp_False).Returns(
                        CommandMarshallerConst.Set_AutoCheck_SO_CSharp_False_Command);
                yield return
                    new TestCaseData(CommandMarshallerConst.Not_Bot_Call).Returns(
                        CommandMarshallerConst.Not_Bot_Call_Command);
                yield return
                    new TestCaseData("bot echo miły").Returns(new Command(Command.Any, "echo",
                        Command.CommandType.Default, "miły"));
                yield return
                    new TestCaseData("bot ?").Returns(new Command(Command.Any, "?", Command.CommandType.Default));
            }
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public Command CheckCommandParsing(string str)
        {
            return parser.GetCommand(Command.Any, str);
        }

        [Test]
        public void SOTest()
        {
            Console.WriteLine(StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(
                new Command("", "", Command.CommandType.Any,
                    "5", "So", "C#")));
        }
    }
}
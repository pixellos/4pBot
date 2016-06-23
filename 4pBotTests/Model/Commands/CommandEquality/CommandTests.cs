using NUnit.Framework;
using System;
using System.Collections;
using pBot.Model.Core;
using pBotTests.Model.Commands.Marshaller;

namespace pBotTests.Model.Commands.CommandEquality
{
    [TestFixture]
    public class CommandTest
    {
        private static string Sender = nameof(Sender);
        private static string Action = nameof(Action);
        private static string Parameter1 = nameof(Parameter1);
        private static string Parameter2 = nameof(Parameter2);

        public static IEnumerable TestCase
        {
            get
            {
                yield return new TestCaseData(
                    new Command(Sender,Action,Command.CommandType.Any,Parameter1),
                    new Command(Sender,Action,Command.CommandType.Any,Parameter1)
                    ).Returns(true).SetName("Senders, Actions, Parameter[0,1] are that same, true");

                yield return new TestCaseData(
                   new Command(Sender, Action, Command.CommandType.Any, Parameter1),
                   new Command(Sender, Action, Command.CommandType.Any, Parameter2)
                   ).Returns(false).SetName("Senders, Actions, are that same, Parameters not, false");

                yield return new TestCaseData(
                   new Command(Command.Any, Action, Command.CommandType.Any, Parameter1),
                   new Command(Sender, Action, Command.CommandType.Any, Parameter2)
                   ).Returns(false).SetName("Senders {Any, !=} , Actions same, Parameters not, false");
            }
        }

        [Test, TestCaseSource(nameof(TestCase))] public bool CommandEqualityByEquals
            (Command first, Command second)
        {
            return first.Equals(second);
        }

        [Test, TestCaseSource(nameof(TestCase))]
        public bool CommandEqualityByOperator(Command first, Command second)
        {
            return first == second;
        }
    }
}
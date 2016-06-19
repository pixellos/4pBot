using System;
using System.Collections;
using NUnit.Framework;
using pBot.Model.Commands;
using pBot.Model.Core;

namespace pBotTests.Model.Commands.Invoker
{
	static class InvokerDataForTest
	{
		public static CommandDelegates.CommandAction ReturnEmptyStringCommandAction => x => string.Empty;
		public static Command EmptyStringCommand => new Command("", "ReturnEmptyString", Command.CommandType.Default);

		public static string FANCY = nameof(FANCY);
		public static CommandDelegates.CommandAction ReturnString_FANCY => x => FANCY;
		public static Command FancyCommand => new Command("", "ReturnEmptyString", Command.CommandType.Default);

		public static IEnumerable TestCases
		{
			get
			{
				yield return new TestCaseData(EmptyStringCommand, ReturnEmptyStringCommandAction).Returns(String.Empty);

				yield return new TestCaseData
					(
						FancyCommand,
						ReturnString_FANCY
					).Returns(FANCY);
			}
		}
	}
}
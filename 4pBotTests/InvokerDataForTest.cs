using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using pBot.Model.Commands;
using static pBot.Model.Commands.CommandDelegates;

namespace pBotTests
{
	static class InvokerDataForTest
	{

		public static CommandAction ReturnEmptyStringCommandAction => x => string.Empty;
		public static Command EmptyStringCommand => new Command("", "ReturnEmptyString", false);


        public static string FANCY = nameof(FANCY);
        public static CommandAction ReturnString_FANCY => x => FANCY;
        public static Command FancyCommand => new Command("", "ReturnEmptyString", false);

        public static IEnumerable TestCases
		{
			get
			{
				yield return new TestCaseData(EmptyStringCommand,ReturnEmptyStringCommandAction).Returns(String.Empty);
                yield return new TestCaseData
                    (
                        FancyCommand,
                        ReturnString_FANCY
                    ).Returns(FANCY).SetName(FANCY);
			}
		}
	}
}
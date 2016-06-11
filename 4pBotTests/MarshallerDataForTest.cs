using System;
using NUnit.Framework;
using pBot.Model.Commands;
using Autofac;
using System.Collections;

namespace pBotTests
{
	public static class MarshallerDataForTest
	{
		public static IEnumerable TestCases
		{
			get
			{
				yield return new TestCaseData(TestsConstatnts.Show_Author).Returns(TestsConstatnts.Show_Author_Command);
				yield return new TestCaseData(TestsConstatnts.Show_Time).Returns(TestsConstatnts.Show_Time_Command);

			}
		}
	}

}
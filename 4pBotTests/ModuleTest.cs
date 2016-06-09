using NUnit.Framework;
using System;
namespace pBotTests
{
	[TestFixture()]
	public class Test
	{

		[TestCase(TestsConstatnts.Show_Author, Result = TestsConstatnts.ExpectedResult_Show_Author)]
		[TestCase(TestsConstatnts.Check_SO_CSharp, Result = TestsConstatnts.ExpectedResult_Check_SO_CSharp)]
		public void TestCase(string command)
		{
			var Command = CommandMarshaler.GetCommand(command);

			var result = CommandInvoker(Command);

			return result;
		}
	}
}


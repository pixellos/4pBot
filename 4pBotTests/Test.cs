using NUnit.Framework;
using System;
namespace pBotTests
{
	public class TestsConstatnts
	{
		public const string TestCommand_Show_Author = "<Test> Bot, show author";
		public const string TestCommand_Show_Time = "<Test> Bot, show time";
		public const string TestCommand_Check_SO_CSharp = "<Test> Bot, chceck SO C#";
		public const string TestCommand_Set_AutoCheck_SO_CSharp_False = "<Test> Bot, don't autocheck SO C#";
		public const string TestCommand_Set_AutoCheck_SO_CSharp_True = "<Test> Bot, autocheck SO C#";

		public const string ExpectedResult_Show_Author = "Pixel";
		public const string ExpectedResult_Check_SO_CSharp = "0 new C# threads";

	}
	[TestFixture()]
	public class Test
	{




		[TestCase(TestCommand_Show_Author, Result = ExpectedResult_Show_Author)]
		[TestCase(TestCommand_Check_SO_CSharp, Result = ExpectedResult_Check_SO_CSharp)]
		public void TestCase(string command)
		{
			var Command = CommandMarshaler.GetCommand(command);

			var result = CommandInvoker(Command);

			return result;
		}
	}
}


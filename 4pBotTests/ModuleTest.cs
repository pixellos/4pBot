using NUnit.Framework;
using System;
using pBot.Model.Commands;
namespace pBotTests
{
	[TestFixture()]
	public class Test
	{
		ICommandMarshaller CommandMarshaler;
		ICommandInvoker CommandInvoker;

		[TestCase(TestsConstatnts.Show_Author, Result = TestsConstatnts.ExpectedResult_Show_Author)]
		[TestCase(TestsConstatnts.Check_SO_CSharp, Result = TestsConstatnts.ExpectedResult_Check_SO_CSharp)]
		public string TestCase(string command)
		{
			var Command = CommandMarshaler.GetCommand(command);

			var result = CommandInvoker.InvokeCommand(Command);

			return result;
		}
	}
}


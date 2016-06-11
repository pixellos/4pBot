using NUnit.Framework;
using System;
using pBot.Model.Commands;
namespace pBotTests
{
	public static class TestsConstatnts
	{
		public const string Show_Author = "Test Bot, show author";
		public readonly static Command Show_Author_Command = new Command("Test", "show", false, "author");
		public const string Show_Time = "Test Bot, show time";
		public readonly static Command Show_Time_Command = new Command("Test", "show", false, "time");
		public const string Check_SO_CSharp = "Test Bot, chceck SO C#";
		public const string Set_AutoCheck_SO_CSharp_False = "Test Bot, don't autocheck SO C#";
		public const string Set_AutoCheck_SO_CSharp_True = "Test Bot, autocheck SO C#";

		public const string ExpectedResult_Show_Author = "Pixel";
		public const string ExpectedResult_Check_SO_CSharp = "0 new C# threads";

	}

}

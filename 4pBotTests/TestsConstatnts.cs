using NUnit.Framework;
using System;
namespace pBotTests
{
	public static class TestsConstatnts
	{
		public const string Show_Author = "<Test> Bot, show author";
		public const string Show_Time = "<Test> Bot, show time";
		public const string Check_SO_CSharp = "<Test> Bot, chceck SO C#";
		public const string Set_AutoCheck_SO_CSharp_False = "<Test> Bot, don't autocheck SO C#";
		public const string Set_AutoCheck_SO_CSharp_True = "<Test> Bot, autocheck SO C#";

		public const string ExpectedResult_Show_Author = "Pixel";
		public const string ExpectedResult_Check_SO_CSharp = "0 new C# threads";

	}

}

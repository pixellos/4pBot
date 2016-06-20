using System;
using pBot;
using NUnit.Framework;
namespace pBotTests

{
	[TestFixture]
	public class SOCheckerTests
	{
		public SOCheckerTests()
		{
		}

		[Test]
		public void Test()
		{
			StackOverflowChecker checker = new StackOverflowChecker();
		}
	}
}


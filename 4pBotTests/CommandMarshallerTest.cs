using System;
using NUnit.Framework;
using pBot.Model.Commands;

namespace pBotTests
{
	[TestFixture()]
	public class CommandMarshallerTest
	{
		ICommandMarshaller marshaller;
		public void CheckMarshalling_Author()
		{
			var command = marshaller.GetCommand(TestsConstatnts.Show_Author);
			Assert.Equals
		}


	}
}


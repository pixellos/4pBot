using System;
using NUnit.Framework;
using pBot.Model.Commands;
using Autofac;
using System.Collections;

namespace pBotTests
{
	[TestFixture()]
	public class CommandMarshallerTest
	{
		ICommandMarshaller marshaller;
		public CommandMarshallerTest()
		{
			var container = pBot.AutofacSetup.GetContainer();

			marshaller = container.Resolve<ICommandMarshaller>();
		}

		[Test, TestCaseSource(typeof(MarshallerDataForTest), "TestCases")]
		public Command CheckMarshalling_Show_Author(string str)
		{
			return marshaller.GetCommand(str);
		}
	}
}




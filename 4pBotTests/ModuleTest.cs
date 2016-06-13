using NUnit.Framework;
using System;
using pBot.Model.Commands;
using Autofac;
using pBot;
using pBot.Model.Commands.General;

namespace pBotTests
{
	[TestFixture()]
	public class Test
	{
		Xmpp xmpp = pBot.AutofacSetup.GetContainer().Resolve<Xmpp>();
		[SetUp]
		public void SetUp()
		{
			xmpp = pBot.AutofacSetup.GetContainer().Resolve<Xmpp>();
		}

		[Test]
		public void Action_ResultIsNot_DefaultResult()
		{
			var msg = "Pixel Bot, Check SO C#";

			var command = xmpp.Marshaller.GetCommand(msg);
			var response = xmpp.Invoker.InvokeCommand(command);

			Assert.AreNotEqual(CommandInvoker.ActionNotFound,response);
		}
	}
}


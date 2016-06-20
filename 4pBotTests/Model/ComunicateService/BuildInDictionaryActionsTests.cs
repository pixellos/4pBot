using System;
using System.Collections;
using Autofac;
using NUnit.Framework;
using pBot.Dependencies;
using pBot.Model;
using pBot.Model.ComunicateService;
using pBot.Model.Core;

namespace pBotTests.Model.ComunicateService
{
	[TestFixture()]
	public class IntegrationTests
	{
	    private XmppFree xmpp;
		[SetUp]
		public void SetUp()
		{
			xmpp = AutofacSetup.GetContainer().Resolve<IXmpp>() as XmppFree;
		}

	    public static IEnumerable ExistAtDictionary
	    {
	        get
	        {
	            yield return new TestCaseData("Pixel", "Bot, Check 4p C#");
	            yield return new TestCaseData("Pixel", "Bot, Check SO C#");
                yield return new TestCaseData("Pixel", "Bot, Auto 10 Check 4p C#");
                yield return new TestCaseData("Pixel", "Bot, don't Auto 10 Check 4p C#");
            }
        }

		[Test,TestCaseSource(nameof(ExistAtDictionary))] //
		public void Action_Exist_AtDictionary(string author, string msg)
		{
			var command = xmpp.Parser.GetCommand(author, msg);
			var response = xmpp.Invoker.InvokeCommand(command);

            Console.WriteLine($"Response: {response}");
			Assert.AreNotEqual(CommandInvoker.ActionNotFound,response);
		}
	}
}
using System.Collections.Generic;
using Autofac;
using pBot.Model;
using pBot.Model.Commands;
using pBot.Model.Commands.Concrete;
using pBot.Model.Commands.RegexMarshaller;
using pBot.Model.ComunicateService;
using pBot.Model.StackOverflowChecker;
using pBot.Model._4pChecker;

namespace pBot.Dependencies
{
	public class AutofacSetup
	{
	    private static IContainer Container = null;
		public static IContainer GetContainer()
		{
		    if (Container == null)
		    {
                var builder = new ContainerBuilder();
                builder.RegisterType<RegexParser>().As<ICommandParser>();
                builder.Register(x => new Dictionary<Command, CommandDelegates.CommandAction>(BuildInCommands.Commands)).AsSelf();

                builder.RegisterType<CommandInvoker>().UsingConstructor(
                    () => new CommandInvoker(new Dictionary<Command, CommandDelegates.CommandAction>())).As<ICommandInvoker>();

                builder.RegisterType<XmppFree>().PropertiesAutowired().As<IXmpp>().SingleInstance();
                builder.RegisterType<_4pChecker>().PropertiesAutowired().AsSelf().SingleInstance();
                builder.RegisterType<AutoRepeater>().PropertiesAutowired().AsSelf().SingleInstance();

                Container = builder.Build();
            }
		    return Container;
		}
	}
}


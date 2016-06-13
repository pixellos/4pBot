using System;
using System.Collections.Generic;
using Autofac;
using pBot.Model.Commands;
using static pBot.Model.Commands.CommandDelegates;
using pBot.Model.Commands.General;
using pBot.Model._4pChecker;

namespace pBot
{
	public class AutofacSetup
	{
	    private static IContainer Container = null;
		public static IContainer GetContainer()
		{
		    if (Container == null)
		    {
                var builder = new ContainerBuilder();


                builder.RegisterType<CommandMarshaller>().
                       As<ICommandMarshaller>();

                builder.Register<Dictionary<Command, CommandAction>>(x => new Dictionary<Command, CommandAction>(commands)).InstancePerDependency();

                builder.RegisterType<CommandInvoker>().UsingConstructor(() => new CommandInvoker(new Dictionary<Command, CommandAction>())).
                       As<Model.Commands.ICommandInvoker>();


                builder.RegisterType<Xmpp>().PropertiesAutowired().AsSelf().SingleInstance();

                builder.RegisterType<_4pChecker>().PropertiesAutowired().AsSelf().SingleInstance();

                builder.RegisterType<AutoRepeater>().PropertiesAutowired().AsSelf().SingleInstance();

                Container = builder.Build();
            }
		    return Container;
		}

	    private static CommandAction action => command => GetContainer().Resolve<AutoRepeater>().DealWithRepeating(command);


        static Dictionary<Command, CommandAction> commands = new Dictionary<Command, CommandAction>()
		{
			{   new Command(Command.Any,"Check",Command.CommandType.Default,"SO",Command.Any), pBot.StackOverflowChecker.GetSingleSORequestWithTagAsParameter},
            {   new Command(Command.Any,"Check",Command.CommandType.Default,"4P",Command.Any), _4pChecker.GetNewestPost },
            {new Command(Command.Any,"Auto",Command.CommandType.Any,Command.Any,Command.Any,Command.Any,Command.Any), action}
		};
	}
}


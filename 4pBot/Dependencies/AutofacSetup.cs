using System.Collections.Generic;
using Autofac;
using pBot.Model.ComunicateService;
using pBot.Model.Core;
using pBot.Model.Core.Abstract;
using pBot.Model.Core.Cache;
using pBot.Model.Core.Data;
using pBot.Model.Functions.HighLevel;
using pBot.Model.Functions._4pChecker;

namespace pBot.Dependencies
{
    public class AutofacSetup
    {
        private static IContainer Container;

        public static IContainer GetContainer()
        {
            if (Container != null) return Container;

            var builder = new ContainerBuilder();
            builder.RegisterType<CachedResponse<Command,string>>().AsSelf();
            builder.RegisterType<RegexParser>().As<ICommandParser>();

            builder.Register(x => new Dictionary<Command, CommandDelegates.CommandAction>(BuildInCommands.Commands))
                .AsSelf();

            builder.Register(x => BuildInCommands.InitializeOrderer()).AsSelf();

            builder.RegisterType<CommandInvoker>().UsingConstructor(
                () => new CommandInvoker(new Dictionary<Command, CommandDelegates.CommandAction>()))
                .As<ICommandInvoker>();

            builder.RegisterType<XmppFree>().PropertiesAutowired().As<IXmpp>().SingleInstance();
            builder.RegisterType<Subscription>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<_4pChecker>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<Repeater>().PropertiesAutowired().AsSelf().SingleInstance();

            Container = builder.Build();
            return Container;
        }
    }
}
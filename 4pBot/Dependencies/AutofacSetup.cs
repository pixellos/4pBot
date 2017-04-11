using System.Collections.Generic;
using Autofac;
using CoreBot;
using Matrix.Xmpp.MessageArchiving;
using pBot.Model.ComunicateService;
using pBot.Model.Facades;
using pBot.Model.Functions;
using pBot.Model.Functions.Checkers.SOChecker;
using pBot.Model.Functions.Checkers._4pChecker;
using pBot.Model.Functions.HighLevel;

namespace pBot.Dependencies
{
    public class AutofacSetup
    {
        private static IContainer Container;
        static AutofacSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<_4Programmers>().PropertiesAutowired();
            builder.RegisterType<StackOverflow>().PropertiesAutowired();
            builder.RegisterType<Downloader4P>().SingleInstance();
            builder.RegisterType<Downloader>().SingleInstance();
            builder.RegisterType<Repeater>().PropertiesAutowired();
            builder.RegisterType<Checker>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<Checker>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<Controllers>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<Actions>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<XmppFree>().PropertiesAutowired().As<IXmpp>().SingleInstance();
            builder.RegisterType<StartupSomethingTodoChangeNameDao>().SingleInstance();
            AutofacSetup.Container = builder.Build();
        }

        public static IContainer GetContainer()
        {
            return AutofacSetup.GetContainer();
        }
    }
}
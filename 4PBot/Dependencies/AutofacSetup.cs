using System.Collections.Generic;
using Autofac;
using CoreBot;
using Matrix.Xmpp.MessageArchiving;
using _4PBot.Model.Facades;
using _4PBot.Model.Functions.StackOverflow;
using _4PBot.Model.Functions._4Programmers;
using _4PBot.Model.ComunicateService;

namespace _4PBot.Dependencies
{
    public class AutofacSetup
    {
        private static IContainer Container;
        static AutofacSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Actions>().AsSelf().SingleInstance();
            builder.RegisterType<_4PBot.Model.Functions._4Programmers.Checker>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<_4PBot.Model.Functions.StackOverflow.Checker>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<_4PBot.Model.Functions.StackOverflow.Downloader>().SingleInstance();
            builder.RegisterType<_4PBot.Model.Functions._4Programmers.Downloader>().SingleInstance();
            builder.RegisterType<_4Programmers>().PropertiesAutowired();
            builder.RegisterType<StackOverflow>().PropertiesAutowired();
            builder.RegisterType<XmppFree>().PropertiesAutowired().As<IXmpp>().SingleInstance();
            AutofacSetup.Container = builder.Build();
        }

        public static IContainer GetContainer()
        {
            return AutofacSetup.GetContainer();
        }
    }
}
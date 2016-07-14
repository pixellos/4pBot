﻿using System.Collections.Generic;
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

        public static IContainer GetContainer()
        {
            if (Container != null)
            {
                return Container;
            }

            var builder = new ContainerBuilder();

            builder.RegisterType<P4Adapter>().PropertiesAutowired();
            builder.RegisterType<SoAdapter>().PropertiesAutowired();

            builder.RegisterType<Downloader4P>().SingleInstance();
            builder.RegisterType<DownloaderSo>().SingleInstance();

            builder.RegisterType<Repeater>().PropertiesAutowired();

            builder.RegisterType<CheckerSO>().PropertiesAutowired().SingleInstance();
            builder.RegisterType<Checker4P>().PropertiesAutowired().SingleInstance();

            builder.RegisterType<Controllers>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<Actions>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<XmppFree>().PropertiesAutowired().As<IXmpp>().SingleInstance();
            builder.RegisterType<SaveManager>().SingleInstance();

            Container = builder.Build();
            return Container;
        }
    }
}
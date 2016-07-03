using System.Collections.Generic;
using Autofac;
using CoreBot;
using pBot.Model.ComunicateService;
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
            builder.RegisterType<DownloaderSo>().SingleInstance();
            builder.RegisterType<CheckerSO>().PropertiesAutowired().SingleInstance();

            builder.RegisterType<Downloader4P>().SingleInstance();
            builder.RegisterType<Checker4P>().PropertiesAutowired().SingleInstance();

            builder.RegisterType<Controllers>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<Orderer>().PropertiesAutowired().AsSelf().SingleInstance();
            builder.RegisterType<XmppFree>().PropertiesAutowired().As<IXmpp>().SingleInstance();

            Container = builder.Build();
            return Container;
        }
    }
}
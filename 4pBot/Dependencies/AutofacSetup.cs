using Autofac;
using CoreBot;
using _4PBot.Model.ComunicateService;
using _4PBot.Model.Facades;
using _4PBot.Model.Functions.HighLevel;

namespace _4PBot.Dependencies
{
    public class AutofacSetup
    {
        private static IContainer Container;
        static AutofacSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<_4Programmers>();
            builder.RegisterType<StackOverflow>();
            builder.RegisterType<Model.Functions._4Programmers.Downloader>().SingleInstance();
            builder.RegisterType<Model.Functions.StackOverflow.Downloader>().SingleInstance();
            builder.RegisterType<Repeater>();
            builder.RegisterType<Model.Functions._4Programmers.Checker>().SingleInstance();
            builder.RegisterType<Model.Functions._4Programmers.Checker>().SingleInstance();
            builder.RegisterType<Controllers>().AsSelf().SingleInstance();
            builder.RegisterType<Actions>().AsSelf().SingleInstance();
            builder.RegisterType<XmppFree>().As<IXmpp>().SingleInstance();
            AutofacSetup.Container = builder.Build();
        }

        public static IContainer GetContainer()
        {
            return AutofacSetup.GetContainer();
        }
    }
}
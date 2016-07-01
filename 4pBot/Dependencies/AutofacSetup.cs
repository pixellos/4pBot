using System.Collections.Generic;
using Autofac;
using pBot.Model.ComunicateService;
using pBot.Model.Functions.HighLevel;
using pBot.Model.Functions.StackOverflowChecker;
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

            builder.Register(x => Controllers.ControllerInitialize()).AsSelf().SingleInstance();
            builder.RegisterType<XmppFree>().PropertiesAutowired().As<IXmpp>().SingleInstance();
            

            Container = builder.Build();
            return Container;
        }
    }
}
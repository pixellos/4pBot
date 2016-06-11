using System;
using Autofac;
using pBot.Model.Commands;
using pBot.Model.Commands.General;

namespace pBot
{
	public class AutofacSetup
	{
		public static IContainer GetContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<CommandMarshaller>().As<ICommandMarshaller>();
			builder.RegisterType<CommandInvoker>().As<Model.Commands.ICommandInvoker>();
			return builder.Build();
		}
	}
}


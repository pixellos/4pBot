using System;
using Autofac;
using pBot.Commands.General;
using pBot.Model.Commands;

namespace pBot
{
	public class AutofacSetup
	{
		public static IContainer GetContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<CommandMarshaller>().As<ICommandMarshaller>();
			return builder.Build();
		}
	}
}


using System;

namespace pBot.Model.Commands
{
	public interface ICommandMarshaller
	{
		Command GetCommand(string text);
	}
}


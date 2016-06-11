using System;
using pBot.Model.Commands;

namespace pBot.Model.Commands
{
	public interface ICommandMarshaller
	{
		Command GetCommand(string text);
	}
}


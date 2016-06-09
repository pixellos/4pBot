using System;
namespace pBot
{
	public interface ICommandMarshaller
	{
		Command GetCommand(string text);
	}
}


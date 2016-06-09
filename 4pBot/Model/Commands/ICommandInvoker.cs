using System;

namespace pBot.Model.Commands
{
	public interface ICommandInvoker
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		/// <returns>The response</returns>
		string InvokeCommand(Command command);
		string AddCommand(Command command, Action<Command> action);
	}
}

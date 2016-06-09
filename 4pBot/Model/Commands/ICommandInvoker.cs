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

		/// <summary>
		/// Adds the temporary command
		/// </summary>
		/// <returns>The temporary command. It wont be stored out of current session.</returns>
		/// <param name="command"></param>
		/// <param name="func">In: Command, Out: String, action invoked when command match</param>
		string AddTemporaryCommand(Command command, Func<Command, string> func);
	}
}

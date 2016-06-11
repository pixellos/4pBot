using System;

namespace pBot.Model.Commands
{
	public static class CommandDelegates
	{
		/// <summary>
		/// Default delegate for Commands 
		/// </summary>
		/// <param name="command">
		/// Accept Command as parameter
		/// </param>
		public delegate string CommandAction(Command command);
	}

}

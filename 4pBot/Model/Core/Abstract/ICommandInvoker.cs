using pBot.Model.Core.Data;

namespace pBot.Model.Core.Abstract
{
    public interface ICommandInvoker
    {
        /// <summary>
        ///     Invokes the command.
        /// </summary>
        /// <returns>The response</returns>
        string InvokeCommand(Data.Command command);


        /// <summary>
        ///     Adds the temporary command. It wont be stored out of current session.
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="func">In: Command, Out: String, action invoked when command match</param>
        void AddTemporaryCommand(Data.Command command, CommandDelegates.CommandAction func);
    }
}
using BotOrder.Old.Core;
using BotOrder.Old.Core.Data;

namespace BotOrder.Abstract.Abstract
{

    public interface ICommandInvoker<T>
    {
        string InvokeCommand(T command);


        /// <summary>
        ///     Adds the temporary command. It wont be stored out of current session.
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="func">In: Command, Out: String, action invoked when command match</param>
        void AddTemporaryCommand(T command, CommandDelegates.CommandAction func);
    }
    public interface ICommandInvoker : ICommandInvoker<Command>
    {
    }
}
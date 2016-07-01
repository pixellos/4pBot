using BotOrder.Old.Core.Data;

namespace BotOrder.Abstract.Abstract
{
    public interface ICommandParser
    {
        Command GetCommand(string author, string text);
    }
}
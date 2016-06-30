namespace pBot.Model.Core.Abstract
{
    public interface ICommandParser
    {
        Data.Command GetCommand(string author, string text);
    }
}
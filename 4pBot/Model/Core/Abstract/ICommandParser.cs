namespace pBot.Model.Core
{
    public interface ICommandParser
    {
        Command GetCommand(string author, string text);
    }
}
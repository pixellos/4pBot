using CoreBot;

namespace _4PBot.Model.Functions
{
    public interface ICommand
    {
        Actions AvailableActions { get; } 
    }
}

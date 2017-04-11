using System.Text;
using CoreBot;
using CoreBot.Mask;

namespace _4PBot.Model.Functions
{
    public class ActionsHelp : ICommand
    {
        public static readonly string BotHelpMessage = "To get help use \"Bot help\" call";
        Actions Actions { get; }
        public ActionsHelp(Actions actions)
        {
            this.Actions = actions;
        }

        public Actions AvailableActions
        {
            get
            {
                var actions = new Actions();
                actions[Builder.Bot().Requried("Help").End()] = x => this.BuildHelpString();
                actions[Builder.Bot().Requried("?").End()] = x => ActionsHelp.BotHelpMessage;
                return actions;
            }
        }

        public string BuildHelpString()
        {
            var result = new StringBuilder();
            result.Append(Actions.HelpHeader);
            foreach (var pair in this.Actions)
            {
                result.Append(Actions.HorizontalSeparator);
                result.AppendLine(pair.Key.Description);
                result.AppendLine(pair.Key.SampleInput);
            }
            return result.ToString();
        }
    }
}
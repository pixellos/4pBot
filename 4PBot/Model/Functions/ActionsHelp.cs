using System;
using System.Text;
using CoreBot;
using CoreBot.Mask;

namespace _4PBot.Model.Functions
{
    public class ActionsHelp : ICommand
    {
        public static readonly string BotHelpMessage = "To get help use \"Bot help\" call";
        private Actions InjectedActions { get; }
        public ActionsHelp(Actions actions)
        {
            this.InjectedActions = actions;
        }

        public string BuildHelpString()
        {
            var result = new StringBuilder();
            result.Append(CoreBot.Actions.HelpHeader);
            foreach (var pair in this.InjectedActions)
            {
                result.Append(Actions.HorizontalSeparator);
                result.AppendLine(pair.Key.Description);
                result.AppendLine(pair.Key.SampleInput);
            }
            return result.ToString();
        }

        public Actions Actions
        {
            get
            {
                var actions = new Actions
                {
                    [Builder.Bot()
                        .Requried("Help")
                        .End()] = x => this.BuildHelpString()
                };
                return actions;
            }
        }
    }
}
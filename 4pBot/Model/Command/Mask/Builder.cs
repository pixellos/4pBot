using System;
using pBot.Model.Commands.Parser.Advanced;
using pBot.Model.Commands.Parser.Command;

namespace pBot.Model.Commands.Parser
{
    public static class CommandBuilder // I'm assuming that every part of command except optional 
    {
        public static Block Prepare()
        {
            return new Block();
        }

        public static Block Bot(this Block block)
        {
            string regexComparer = @"\w+";
            string botNick = "Bot Nick";
            string Bot = nameof(Bot);

            return block.AddToCommandBlock(regexComparer, botNick, Bot, Bot,ArgumentOptions.Core);
        }

        public static Block ThenNonWhiteSpaceString(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>\S+)", $"Non whitespace string to {sectionName}", sectionName, sampleInput,ArgumentOptions.Required);
        }
        public static Block ThenWord(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>\w+)", $"String to {sectionName}", sectionName, sampleInput,ArgumentOptions.Required);
        }

        public static Block ThenEverythingToEndOfLine(this Block block, string sectionName,string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>((\S+\s*)+))", $"Everything to {sectionName}", sectionName, sampleInput,ArgumentOptions.Optional);
        }

        private static Block AddToCommandBlock(this Block block, string regexComparer, string description, string sectionName, string sampleInput,ArgumentOptions argumentOptions)
        {
            const string separatorPattern = @"\W";
            block.RegexString += regexComparer + separatorPattern;
            block.Arguments.Add(new Argument(argumentOptions,sectionName));
            block.SampleInput += sampleInput + " ";
            block.Description += description;
            return block;
        }

        public static Mask FinalizeCommand(this Block block)
        {
            return new Mask(block);
        }
    }
}
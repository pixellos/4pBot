using System;
using pBot.Model.Commands.Parser.Advanced;

namespace pBot.Model.Commands.Parser
{
    public static class CommandBuilder // I'm assuming that every part of command except optional 
    {
        public static Block Prepare()
        {
            return new Block();
        }

        public static Block Bot(this Block Block)
        {
            string regexComparer = @"\w+";
            string botNick = "Bot Nick";
            string Bot = nameof(Bot);

            return Block.AddToBlock(regexComparer, botNick, Bot, Bot,ArgumentOptions.Core);
        }

        public static Block ThenNonWhiteSpaceString(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToBlock($@"(?<{sectionName}>\S+)", $"Non whitespace string to {sectionName}", sectionName, sampleInput,ArgumentOptions.Required);
        }
        public static Block ThenWord(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToBlock($@"(?<{sectionName}>\w+)", $"String to {sectionName}", sectionName, sampleInput,ArgumentOptions.Required);
        }

        public static Block ThenEverythingToEndOfLine(this Block block, string sectionName,string sampleInput)
        {
            return block.AddToBlock($@"(?<{sectionName}>((\S+\s*)+))", $"Everything to {sectionName}", sectionName, sampleInput,ArgumentOptions.Optional);
        }

        private static Block AddToBlock(this Block Block, string regexComparer, string description, string sectionName, string sampleInput,ArgumentOptions argumentOptions)
        {
            const string separatorPattern = @"\W";
            Block.RegexString += regexComparer + separatorPattern;
            Block.Arguments.Add(new Argument(argumentOptions,sectionName));
            Block.SampleInput += sampleInput + " ";
            Block.Description += description;
            return Block;
        }

        public static Mask FinalizeCommand(this Block block)
        {
            return new Mask(block);
        }
    }
}
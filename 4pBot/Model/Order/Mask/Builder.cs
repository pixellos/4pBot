using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace pBot.Model.Order.Mask
{
    public static class Builder // I'm assuming that every part of command except optional 
    {
        public static Block Prepare()
        {
            return new Block();
        }

        public static Block Bot(this Block block)
        {
            string regexComparer = @"Bot";
            string botNick = "Bot Nick";
            string Bot = nameof(Bot);

            return block.AddToCommandBlock(regexComparer, botNick, Bot, Bot,ArgumentOptions.Core, String.Empty);
        }

        public static Block ThenNonWhiteSpaceString(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>\S+)", $"Non whitespace string to {sectionName}", sectionName, sampleInput,ArgumentOptions.Required);
        }

        public static Block ThenRequried(this Block block, string requestedInput)
        {
            return block.AddToCommandBlock($"{requestedInput}", $"Requried {requestedInput}", requestedInput,
                requestedInput, ArgumentOptions.Core);
        }

        public static Block ThenWord(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>\w+)", $"String to {sectionName}", sectionName, sampleInput,ArgumentOptions.Required);
        }

        public static Block ThenEverythingToEndOfLine(this Block block, string sectionName,string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>((\S+\s*)+))", $"Everything to {sectionName}", sectionName, sampleInput,ArgumentOptions.Optional);
        }

        private static Block AddToCommandBlock(this Block block, string regexComparer, string description, string sectionName, string sampleInput,ArgumentOptions argumentOptions,string preSampleInputSeparator = " ")
        {
            const string separatorPattern = @"\W*";
            block.SampleInput += preSampleInputSeparator + sampleInput;
            block.RegexString += regexComparer + separatorPattern;
            block.Arguments.Add(new Argument(argumentOptions,sectionName));
            block.Description += description;
            return block;
        }

        public static Mask FinalizeCommand(this Block block)
        {
            return new Mask(block);
        }
    }
}
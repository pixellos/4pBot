using System;

namespace CoreBot.Mask
{
    public static class Builder // I'm assuming that every part of command except optional 
    {
        public static Block Prepare()
        {
            return new Block() {RegexString = @"^"};
        }

        [Obsolete("Use Bot() instead without Prepare()")]
        public static Block Bot(this Block block)
        {
            string regexComparer = @"Bot";
            string botNick = "(Nick of Bot)";
            string Bot = nameof(Bot);

            return block.AddToCommandBlock(regexComparer, botNick, Bot, Bot, ArgumentOptions.Core, String.Empty);
        }

        public static Block Bot()
        {
            var block = Prepare();
#pragma warning disable 618
            return block.Bot();
#pragma warning restore 618
        }


        public static Block StartsWith(string startsWith)
        {
            return new Block().AddToCommandBlock($"^{startsWith}", $"({startsWith})", startsWith, startsWith,
                ArgumentOptions.Core, "");
        }
        public static Block ThenString(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>\S+)", $"({sectionName} : string)", sectionName, sampleInput,ArgumentOptions.Required);
        }

        public static Block Requried(this Block block, string requestedInput)
        {
            return block.AddToCommandBlock($"{requestedInput}",$"({requestedInput})", requestedInput,
                requestedInput, ArgumentOptions.Core);
        }

        public static Block ThenWord(this Block block, string sectionName, string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>\w+)", $"({sectionName} : word)", sectionName, sampleInput,ArgumentOptions.Required);
        }

        public static Block ThenEverythingToEndOfLine(this Block block, string sectionName,string sampleInput)
        {
            return block.AddToCommandBlock($@"(?<{sectionName}>((\S+\s*)+))", $"({sectionName}: to EOL [Optional])", sectionName, sampleInput,ArgumentOptions.Optional);
        }

        public static Block ThenEverythingToEndOfLine(this Block block, string sectionNameandSampleInput)
        {
            return block.ThenEverythingToEndOfLine(sectionNameandSampleInput, sectionNameandSampleInput);
        }

        private static Block AddToCommandBlock(this Block block, string regexComparer, string description, string sectionName, string sampleInput,ArgumentOptions argumentOptions,string preSampleInputSeparator = " ")
        {
            const string separatorPattern = @"\W*";
            block.SampleInput += preSampleInputSeparator + sampleInput;
            block.RegexString += regexComparer + separatorPattern;
            block.Arguments.Add(new Argument(argumentOptions,sectionName));
            block.Description += description + " ";
            return block;
        }

        public static Mask End(this Block block)
        {
            return new Mask(block);
        }
    }
}
using BotOrder.Old.Core.Data;

namespace pBotTests.Marshaller
{
    public class CommandMarshallerConst
    {
        public const string Show_Author = "Bot, show author";
        public const string Show_Time = "Bot, show time";
        public const string Set_AutoCheck_SO_CSharp_False = "Bot, don't autocheck SO C#";
        public const string Set_AutoCheck_SO_CSharp_True = "Bot, autocheck SO C#";
        public const string Not_Bot_Call = "Test nieBot, show author";

        public static readonly Command Show_Author_Command = new Command(Command.Any, "show",
            Command.CommandType.Default, "author");

        public static readonly Command Show_Time_Command = new Command(Command.Any, "show", Command.CommandType.Default,
            "time");

        public static readonly Command Set_AutoCheck_SO_CSharp_False_Command = new Command(Command.Any, "autocheck",
            Command.CommandType.Negation, "SO", "C#");

        public static readonly Command Set_AutoCheck_SO_CSharp_True_Command = new Command(Command.Any, "autocheck",
            Command.CommandType.Default, "SO", "C#");

        public static readonly Command Not_Bot_Call_Command = Command.Empty();
    }
}
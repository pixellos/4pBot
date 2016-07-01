using System;
using System.Collections.Generic;
using Autofac;
using BotOrder;
using static BotOrder.Mask.Builder;
using pBot.Model.ComunicateService;
using pBot.Model.Core;
using pBot.Model.Core.Data;
using pBot.Model.Functions.HighLevel;
using pBot.Model.Functions.StackOverflowChecker;
using pBot.Model.Functions._4pChecker;

namespace pBot.Dependencies
{
    public class BuildInCommands
    {

        public static OrderDoer InitializeOrderer()
        {
            OrderDoer orderDoer = new OrderDoer();

            var TagString = "Tag";
            orderDoer.AddTemporaryCommand(
                Bot().ThenRequried("SO").ThenNonWhiteSpaceString(TagString,"C#").FinalizeCommand(),
                x=>StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString]));

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequried("4P").ThenNonWhiteSpaceString(TagString,"C++").FinalizeCommand(),
                x=>_4pChecker.GetNewestPost(x.MatchedResult[TagString]));

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequried("Room").ThenWord("RoomName", "Help").FinalizeCommand(),
                x => ChangesRoom(x.MatchedResult["RoomName"]));

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequried("?").FinalizeCommand(),
                x=>orderDoer.GetHelpAboutAllCommands());
       
            return orderDoer;
        }

        public static Dictionary<Command, CommandDelegates.CommandAction> Commands = new Dictionary
            <Command, CommandDelegates.CommandAction>
        {
            {
                new Command(Command.Any, "Check", Command.CommandType.Default,
                    "SO", Command.Any),
                StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter
            },
            {
                new Command(Command.Any, "Check", Command.CommandType.Default,
                    "4P", Command.Any),
                _4pChecker.GetNewestPost
            },
            {new Command(Command.Any, "Auto", Command.CommandType.Default, "current", "tasks"), Current},

            {
                new Command(Command.Any, "Auto", Command.CommandType.Any,
                    Command.Any, Command.Any, Command.Any, Command.Any, Command.Any, Command.Any),
                RepeatCommand
            },
            {new Command(Command.Any, "Room", Command.CommandType.Any, Command.Any), ChangeRoom},
            {
                new Command(Command.Any, "Subscribe", Command.CommandType.Any, Command.Any, Command.Any, Command.Any,
                    Command.Any, Command.Any),
                SubscribeCommand
            },
            {
                new Command(Command.Any, "?", Command.CommandType.Any, Command.Any), command =>
                    "Available commands:\n" +
                    "Command, parameters, additional parameters\n" +
                    "Check (SO|4P) (C#/Java/Haskell/Etc...) //One time webpage check \n" +
                    "Auto (from 5 to 2^31 integer) ([Check ...]|[Show ...]) //Auto check\n" +
                    "Subscribe ([Check ...]|[Show ...]) //your personal info feed "
            }
        };

        private static CommandDelegates.CommandAction ChangeRoom
            => command => AutofacSetup.GetContainer().Resolve<IXmpp>().ChangeRoom(command);


        private static Func<string,string> ChangesRoom
            => str => AutofacSetup.GetContainer().Resolve<IXmpp>().ChangeRoom(str);


        private static CommandDelegates.CommandAction RepeatCommand
            => command => AutofacSetup.GetContainer().Resolve<Repeater>().DealWithRepeating(command);

        private static CommandDelegates.CommandAction SubscribeCommand
            => command => AutofacSetup.GetContainer().Resolve<Subscription>().DealWithRepeating(command);

        private static CommandDelegates.CommandAction Current
            => command => AutofacSetup.GetContainer().Resolve<Repeater>().GetCurrentTasks();
    }
}
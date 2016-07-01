using System;
using System.Collections.Generic;
using Autofac;
using BotOrder;
using BotOrder.Old.Core;
using BotOrder.Old.Core.Data;
using static BotOrder.Mask.Builder;
using pBot.Model.ComunicateService;
using pBot.Model.Functions.HighLevel;
using pBot.Model.Functions.StackOverflowChecker;
using pBot.Model.Functions._4pChecker;

namespace pBot.Dependencies
{
    public class Controllers
    {
        public static OrderDoer ControllerInitialize()
        {
            OrderDoer orderDoer = new OrderDoer();

            SOController(orderDoer);
            _4PController(orderDoer);

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("Room").ThenWord("RoomName", "Help").FinalizeCommand(),
                x => ChangesRoom(x.MatchedResult["RoomName"]));

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("?").FinalizeCommand(),
                x => orderDoer.GetHelpAboutAllCommands());

            return orderDoer;
        }

        private static void SOController(OrderDoer orderDoer)
        {
            var SOContinueRequest = new ContinuesRequest();
            var TagString = "Tag";
            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("SO").ThenNonWhiteSpaceString(TagString, "C#").FinalizeCommand(), //Route
                x => StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString])); //Action

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("SO").ThenNonWhiteSpaceString(TagString, "Java").ThenRequired("Repeat").ThenNonWhiteSpaceString("Delay", "5").FinalizeCommand(),
                x => SOContinueRequest.AddRequest(x.MatchedResult[TagString],
                () => StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString]),
                Int32.Parse(x.MatchedResult["Delay"])));

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("Dont").ThenRequired("SO").ThenNonWhiteSpaceString(TagString, "Java").FinalizeCommand(),
                x => SOContinueRequest.RemoveRequest(x.MatchedResult[TagString]));
        }

        private static void _4PController(OrderDoer orderDoer)
        {
            var _4PContinueRequest = new ContinuesRequest();
            var TagString = "Tag";

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("4P").ThenNonWhiteSpaceString(TagString, "C++").FinalizeCommand(),
                x => _4pChecker.GetNewestPost(x.MatchedResult[TagString]));

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("4P").ThenNonWhiteSpaceString(TagString, "Java").ThenRequired("Repeat").ThenNonWhiteSpaceString("Delay", "5").FinalizeCommand(),
                x => _4PContinueRequest.AddRequest(x.MatchedResult[TagString],
                () => StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString]),
                Int32.Parse(x.MatchedResult["Delay"])));

            orderDoer.AddTemporaryCommand(
                Bot().ThenRequired("Dont").ThenRequired("4P").ThenNonWhiteSpaceString(TagString, "Java").FinalizeCommand(),
                x => _4PContinueRequest.RemoveRequest(x.MatchedResult[TagString]));
        }

     
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
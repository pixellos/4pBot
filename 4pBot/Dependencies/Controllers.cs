﻿using System;
using BotOrder;
using pBot.Model;
using static BotOrder.Mask.Builder;
using pBot.Model.ComunicateService;
using pBot.Model.Functions.HighLevel;
using pBot.Model.Functions.StackOverflowChecker;
using pBot.Model.Functions._4pChecker;

namespace pBot.Dependencies
{
    public class Controllers
    {
        public IXmpp Xmpp { get; set; }
        public Orderer Orderer { get; set; }
        public StackOverflowHtmlChecker StackOverflowHtmlChecker { get; set; }
        public _4pChecker _4PChecker { get; set; }
        private Action<string> SendCurrentXmpp => str => Xmpp.SendIfNotNull(str);

        private Func<string, string> ChangesRoom
            => str => Xmpp.ChangeRoom(str);

        private static Func<CachedResponse<string, string>> CachedResponse => () => new CachedResponse<string, string>();


        public Orderer ControllerInitialize()
        {
            SOController(Orderer);
            _4PController(Orderer);
            RoomController(Orderer);
            InfoController(Orderer);

            return Orderer;
        }

        private void InfoController(Orderer orderer)
        {
            orderer.AddTemporaryCommand(
                Bot().Then("?").FinalizeCommand(),
                x => orderer.GetHelpAboutAllCommands());

        }

        private void RoomController(Orderer orderer)
        {
            orderer.AddTemporaryCommand(
                Bot().Then("Room").ThenWord("RoomName", "Help").FinalizeCommand(),
                x => ChangesRoom(x.MatchedResult["RoomName"]));
        }

        private void SOController(Orderer orderer)
        {
            var SOContinueRequest = new ContinuesRequest() {SendCommand = SendCurrentXmpp,CachedResponse = CachedResponse()};
            var TagString = "Tag";
            string Delay = nameof(Delay);


            orderer.AddTemporaryCommand(
                Bot().Then("SO").Then("Repeat").Then("Show").FinalizeCommand(),
                x => SOContinueRequest.CheckRequests());


            orderer.AddTemporaryCommand(
                Bot().Then("SO").ThenNonWhiteSpaceString(TagString, "Java").Then("Repeat").ThenNonWhiteSpaceString(Delay, "5").FinalizeCommand(),
                x => SOContinueRequest.AddRequest(x.MatchedResult[TagString],
                    () => 
                        StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString]),
                        Int32.Parse(x.MatchedResult[Delay])
                        )
                );



            orderer.AddTemporaryCommand(
                Bot().Then("Dont").Then("SO").ThenNonWhiteSpaceString(TagString, "Java").FinalizeCommand(),
                x => SOContinueRequest.RemoveRequest(x.MatchedResult[TagString]));

            orderer.AddTemporaryCommand(
                Bot().Then("SO").ThenNonWhiteSpaceString(TagString, "C#").FinalizeCommand(), //Route
                x => StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString])); //Action
        }

        private void _4PController(Orderer orderer)
        {
            var _4PContinueRequest = new ContinuesRequest() {SendCommand = SendCurrentXmpp,CachedResponse = CachedResponse()};
            var TagString = "Tag";


            orderer.AddTemporaryCommand(
             Bot().Then("4P").Then("Repeat").Then("Show").FinalizeCommand(),
             x => _4PContinueRequest.CheckRequests());


            orderer.AddTemporaryCommand(
                Bot().Then("4P").ThenNonWhiteSpaceString(TagString, "Java").Then("Repeat").ThenNonWhiteSpaceString("Delay", "5").FinalizeCommand(),
                x => _4PContinueRequest.AddRequest(x.MatchedResult[TagString],
                    () => 
                        _4PChecker.GetNewestPost(x.MatchedResult[TagString]),
                        Int32.Parse(x.MatchedResult["Delay"])
                        )
                );

            orderer.AddTemporaryCommand(
                Bot().Then("Dont").Then("4P").ThenNonWhiteSpaceString(TagString, "Java").FinalizeCommand(),
                x => _4PContinueRequest.RemoveRequest(x.MatchedResult[TagString]));


            orderer.AddTemporaryCommand(
                Bot().Then("4P").ThenNonWhiteSpaceString(TagString, "C++").FinalizeCommand(),
                x => _4PChecker.GetNewestPost(x.MatchedResult[TagString]));
        }
    }
}
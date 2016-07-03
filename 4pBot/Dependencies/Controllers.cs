using System;
using CoreBot;
using pBot.Model;
using static CoreBot.Mask.Builder;
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
        public Checker4P Checker4P { get; set; }
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
                Bot().Then("?").End(),
                x => orderer.GetHelpAboutAllCommands());

        }

        private void RoomController(Orderer orderer)
        {
            orderer.AddTemporaryCommand(
                Bot().Then("Room").ThenWord("RoomName", "Help").End(),
                x => ChangesRoom(x.MatchedResult["RoomName"]));
        }

        private void SOController(Orderer orderer)
        {
            var SOContinueRequest = new ContinuesRequest() {SendCommand = SendCurrentXmpp,CachedResponse = CachedResponse()};
            var TagString = "Tag";
            string Delay = nameof(Delay);


            orderer.AddTemporaryCommand(
                Bot().Then("SO").Then("Repeat").Then("Show").End(),
                x => SOContinueRequest.CheckRequests());


            orderer.AddTemporaryCommand(
                Bot().Then("SO").ThenNonWhiteSpaceString(TagString, "Java").Then("Repeat").ThenNonWhiteSpaceString(Delay, "5").End(),
                x => SOContinueRequest.AddRequest(x.MatchedResult[TagString],
                    () => 
                        StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString]),
                        Int32.Parse(x.MatchedResult[Delay])
                        )
                );



            orderer.AddTemporaryCommand(
                Bot().Then("Dont").Then("SO").ThenNonWhiteSpaceString(TagString, "Java").End(),
                x => SOContinueRequest.RemoveRequest(x.MatchedResult[TagString]));

            orderer.AddTemporaryCommand(
                Bot().Then("SO").ThenNonWhiteSpaceString(TagString, "C#").End(), //Route
                x => StackOverflowHtmlChecker.GetSingleSORequestWithTagAsParameter(x.MatchedResult[TagString])); //Action
        }

        private void _4PController(Orderer orderer)
        {
            var _4PContinueRequest = new ContinuesRequest() {SendCommand = SendCurrentXmpp,CachedResponse = CachedResponse()};
            var TagString = "Tag";


            orderer.AddTemporaryCommand(
             Bot().Then("4P").Then("Repeat").Then("Show").End(),
             x => _4PContinueRequest.CheckRequests());


            orderer.AddTemporaryCommand(
                Bot().Then("4P").ThenNonWhiteSpaceString(TagString, "Java").Then("Repeat").ThenNonWhiteSpaceString("Delay", "5").End(),
                x => _4PContinueRequest.AddRequest(x.MatchedResult[TagString],
                    () => 
                        Checker4P.GetNewestPost(x.MatchedResult[TagString]),
                        Int32.Parse(x.MatchedResult["Delay"])
                        )
                );

            orderer.AddTemporaryCommand(
                Bot().Then("Dont").Then("4P").ThenNonWhiteSpaceString(TagString, "Java").End(),
                x => _4PContinueRequest.RemoveRequest(x.MatchedResult[TagString]));


            orderer.AddTemporaryCommand(
                Bot().Then("4P").ThenNonWhiteSpaceString(TagString, "C++").End(),
                x => Checker4P.GetNewestPost(x.MatchedResult[TagString]));
        }
    }
}
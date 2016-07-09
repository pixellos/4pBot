using System;
using CoreBot;
using CoreBot.Mask;
using pBot.Model;
using static CoreBot.Mask.Builder;
using pBot.Model.ComunicateService;
using pBot.Model.DataStructures;
using pBot.Model.Facades;
using pBot.Model.Functions;
using pBot.Model.Functions.Checkers.SOChecker;
using pBot.Model.Functions.Checkers._4pChecker;
using static System.Int32;

namespace pBot.Dependencies
{
    public class Controllers
    {
        public IXmpp Xmpp { get; set; }
        public Actions Actions { get; set; }
        public CheckerSO CheckerSo { get; set; }
        public Checker4P Checker4P { get; set; }
        public SaveManager SaveManager { get; set; }

        public P4Adapter P4Adapter { get; set; }
        public SoAdapter SoAdapter { get; set; }

        private Action<string> SendCurrentXmpp => str => Xmpp.SendIfNotNull(str);

        private Func<string, string> ChangesRoom
            => str => Xmpp.ChangeRoom(str);

        private static Func<CachedResponse<string, string>> CachedResponse => () => new CachedResponse<string, string>();

        public Actions ControllerInitialize()
        {

            ProgramersWebsiteQuotations(Actions,"4p",P4Adapter);
            ProgramersWebsiteQuotations(Actions,"SO",SoAdapter);

            RoomController(Actions);


            SaySomethingToController(Actions);
            InfoController(Actions);

            return Actions;
        }

        private void InfoController(Actions actions)
        {
            actions[Bot().Requried("Help").End()] = x => actions.GetHelpAboutActions(); 
            actions[Bot().Requried("?").End()] = x => "To get help use \"Bot help\" call";
        }

        private void SaySomethingToController(Actions actions)
        {
            actions[Bot().Requried("Save").ThenWord("NickName", "Pixel").ThenEverythingToEndOfLine("Message").End()] =
                result =>
                {
                    SaveManager.Save(result.MatchedResult["NickName"], result.MatchedResult["Message"]);
                    return "Saved!";
                };

            actions[StartsWith("!").ThenWord("NickName", "Pixel").End()] = result => SaveManager.Get(result["NickName"]);
        }

        private void RoomController(Actions actions)
        {
            actions[Bot().Requried("Room").ThenWord("RoomName", "Help").End()] = result => ChangesRoom(result["RoomName"]);
        }

        private void ProgramersWebsiteQuotations(Actions actions, string forumPrefix, IProgrammingSitesAdapter adapter)
        {
            var Tag = "TagLiteral";

            actions[Bot().Requried(forumPrefix).ThenString(Tag, "Java").Requried("Repeat").Requried("Tag").End()] =
                result => adapter.HotPostsStream(result[Tag]);

            actions[Bot().Requried("Dont").Requried(forumPrefix).ThenString(Tag, "Java").Requried("Repeat").Requried("Tag").End()] =
                x => adapter.HotPostsStreamStop(x[Tag]);

            actions[Bot().Requried(forumPrefix).ThenString(Tag, "Java").End()] =
                result => adapter.HotPost(result[Tag]);

        }
    }
}
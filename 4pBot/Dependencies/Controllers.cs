using System;
using CoreBot;
using CoreBot.Mask;
using static CoreBot.Mask.Builder;
using pBot.Model.ComunicateService;
using pBot.Model.DataStructures;
using pBot.Model.Facades;
using pBot.Model.Functions;
using pBot.Model.Functions.Checkers.SOChecker;
using pBot.Model.Functions.Checkers._4pChecker;
using LiteDB;
using System.Linq;

namespace pBot.Dependencies
{
    public class Controllers
    {
        public IXmpp Xmpp { get; set; }
        public Actions Actions { get; set; }
        public Checker CheckerSo { get; set; }
        public Checker Checker4P { get; set; }
        public StartupSomethingTodoChangeNameDao SaveManager { get; set; }
        public P4 P4Adapter { get; set; }
        public SoAdapter SoAdapter { get; set; }

        private Action<string> SendCurrentXmpp => str => Xmpp.SendIfNotNull(str);

        private Func<string, string> ChangesRoom => str => Xmpp.ChangeRoom(str);

        private static Func<CachedResponse<string, string>> CachedResponse => () => new CachedResponse<string, string>();

        public Actions ControllerInitialize()
        {
            this.ProgramersWebsiteQuotations(Actions, "4p", P4Adapter);
            this.ProgramersWebsiteQuotations(Actions, "SO", SoAdapter);
            this.RoomController(Actions);
            this.SaySomethingToController(Actions);
            this.InfoController(Actions);
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
                    using (var db = new LiteDatabase(nameof(SaySomethingToController)))
                    {
                        var savedMessage = new UserMessage()
                        {
                            Message = result.MatchedResult["Message"],
                            User = result.MatchedResult["NickName"]
                        };
                        var collection = db.GetCollection<UserMessage>();
                        collection.Delete(x => x.User == result["NickName"]);
                        collection.EnsureIndex(x => x.Key, true);
                        collection.Insert(savedMessage);
                    }
                    return "Saved!";
                };
            actions[StartsWith("!").ThenWord("NickName", "Pixel").End()] = result =>
            {
                using (var db = new LiteDatabase(nameof(SaySomethingToController)))
                {
                    var collection = db.GetCollection<UserMessage>();
                    var messages = collection.Find(x => x.User == result["NickName"]);
                    if (messages.Count() > 1)
                    {
                        throw new Exception("There should be no more than 1 matching entry.");
                    }
                    return messages.Single().Message;
                }
            };
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
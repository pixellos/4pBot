using System;
using System.Linq;
using CoreBot;
using CoreBot.Mask;
using LiteDB;
using _4PBot.Model.ComunicateService;
using _4PBot.Model.DataStructures;
using _4PBot.Model.Functions;

namespace _4PBot.Dependencies
{
    public class Controllers
    {
        public IXmpp Xmpp { get; set; }
        public Actions Actions { get; set; }

        private Func<string, string> ChangesRoom => str => this.Xmpp.ChangeRoom(str);


        public Actions ControllerInitialize()
        {
            this.RoomController(this.Actions);
            this.SaySomethingToController(this.Actions);
            return this.Actions;
        }

        private void SaySomethingToController(Actions actions)
        {
            actions[Builder.Bot().Requried("Save").ThenWord("NickName", "Pixel").ThenEverythingToEndOfLine("Message").End()] =
                result =>
                {
                    using (var db = new LiteDatabase(nameof(this.SaySomethingToController)))
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
            actions[Builder.StartsWith("!").ThenWord("NickName", "Pixel").End()] = result =>
            {
                using (var db = new LiteDatabase(nameof(this.SaySomethingToController)))
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
            actions[Builder.Bot().Requried("Room").ThenWord("RoomName", "Help").End()] = result => this.ChangesRoom(result["RoomName"]);
        }
    }
}
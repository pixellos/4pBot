using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreBot;
using CoreBot.Mask;
using LiteDB;

namespace _4PBot.Model.Functions
{
    public class Messages : ICommand
    {
        public Actions AvailableActions
        {
            get
            {
                var actions = new Actions();
                this.Save(actions);
                this.Read(actions);
                return actions;
            }
        }

        private void Read(Actions actions)
        {
            actions[Builder.StartsWith("!").ThenWord("NickName", "Pixel").End()] = result =>
            {
                using (var db = new LiteDatabase(nameof(Messages)))
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

        private void Save(Actions actions)
        {
            actions[Builder.Bot().Requried("Save").ThenWord("NickName", "Pixel").ThenEverythingToEndOfLine("Message").End()] = result =>
            {
                using (var db = new LiteDatabase(nameof(Messages)))
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
        }
    }
}

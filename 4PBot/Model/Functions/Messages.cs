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
        private static readonly string ConnectionString = $"{nameof(Messages)}.db";
        enum Words
        {
            NickName,
            Message,
            Save

        }

        private void AppendRead(Actions actions)
        {
            actions[Builder.StartsWith("!").ThenWord(nameof(Words.NickName), "Pixel").End()] = result =>
            {
                using (var db = new LiteDatabase(Messages.ConnectionString))
                {
                    var collection = db.GetCollection<UserMessage>();
                    var messages = collection.Find(x => x.User == result[nameof(Words.NickName)]);
                    if (messages.Count() > 1)
                    {
                        throw new Exception("There should be no more than 1 matching entry.");
                    }
                    return messages.SingleOrDefault()?.Message ?? String.Empty;
                }
            };
        }

        private void AppendSave(Actions actions)
        {
            actions[Builder.Bot().Requried(nameof(Words.Save)).ThenWord(nameof(Words.NickName), "Pixel").ThenEverythingToEndOfLine(nameof(Words.Message)).End()] = result =>
            {
                using (var db = new LiteDatabase(Messages.ConnectionString))
                {
                    var savedMessage = new UserMessage()
                    {
                        Message = result.MatchedResult[nameof(Words.Message)],
                        User = result.MatchedResult[nameof(Words.NickName)]
                    };
                    var collection = db.GetCollection<UserMessage>();
                    collection.Delete(x => x.User == result[nameof(Words.NickName)]);
                    collection.EnsureIndex(x => x.User);
                    collection.Insert(savedMessage);
                }
                return "Saved!";
            };
        }

        public Actions Actions
        {
            get
            {
                var a = new Actions();
                this.AppendRead(a);
                this.AppendSave(a);
                return a;
            }
        }
    }
}

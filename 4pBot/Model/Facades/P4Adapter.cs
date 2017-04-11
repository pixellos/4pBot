using System;
using CoreBot;
using pBot.Model.ComunicateService;
using pBot.Model.DataStructures;
using pBot.Model.Functions;
using pBot.Model.Functions.Checkers._4pChecker;
using pBot.Model.Functions.HighLevel;

namespace pBot.Model.Facades
{
    public class P4 : IProgrammingSitesAdapter, ICommand
    {
        private Checker Checker { get; }
        private  Repeater Repeater { get; }
        private CachedResponse<string, string> Cache = new CachedResponse<string, string>();
        public Actions AvailableActions => throw new NotImplementedException();

        public P4(Checker checker, Repeater repeater)
        {
            this.Checker = checker;
            this.Repeater = repeater;
        }

        public string HotPost(string tag)
        {
            return Checker.GetLastMessagesByTag(tag);
        }

        public string HotPostsStream(string tag)
        {
            return Repeater.Add(tag, 10, () => Checker.GetLastMessagesByTag(tag,false));
        }

        public string HotPostsStreamStop(string tag)
        {
            return Repeater.RemoveRequest(tag);
        }

        public string NewThreads(string forumName)
        {
            return Checker.GetLastPostAtCategory(forumName);
        }

        public string NewThreadsStream(string forumName)
        {
            return Repeater.Add(forumName + "Thread", 10, () => Checker.GetLastPostAtCategory(forumName));
        }

        public string NewThreadsStreamStop(string forumName)
        {
            return Repeater.RemoveRequest(forumName + "Thread");
        }
    }
}
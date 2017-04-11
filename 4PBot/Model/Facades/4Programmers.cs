using CoreBot;
using _4PBot.Model.DataStructures;
using _4PBot.Model.Functions;
using _4PBot.Model.Functions.HighLevel;
using _4PBot.Model.Functions._4Programmers;

namespace _4PBot.Model.Facades
{
    public class _4Programmers : IProgrammingSitesAdapter, ICommand
    {
        public static string Name = nameof(_4Programmers).Substring(1);
        private Checker Checker { get; }
        private Repeater Repeater { get; }
        private CachedResponse<string, string> Cache = new CachedResponse<string, string>();
        public Actions AvailableActions
        {
            get
            {
                var actions = this.ConstructStandardProgrammingSitesQuotations(_4Programmers.Name);
                return actions;
            }
        }

        public _4Programmers(Checker checker, Repeater repeater)
        {
            this.Checker = checker;
            this.Repeater = repeater;
        }

        public string HotPost(string tag)
        {
            return this.Checker.GetLastMessagesByTag(tag);
        }

        public string HotPostsStream(string tag)
        {
            return this.Repeater.Add(tag, 10, () => this.Checker.GetLastMessagesByTag(tag, false));
        }

        public string HotPostsStreamStop(string tag)
        {
            return this.Repeater.RemoveRequest(tag);
        }

        public string NewThreads(string forumName)
        {
            return this.Checker.GetLastPostAtCategory(forumName);
        }

        public string NewThreadsStream(string forumName)
        {
            return this.Repeater.Add(forumName + "Thread", 10, () => this.Checker.GetLastPostAtCategory(forumName));
        }

        public string NewThreadsStreamStop(string forumName)
        {
            return this.Repeater.RemoveRequest(forumName + "Thread");
        }
    }
}
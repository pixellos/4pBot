using pBot.Model.ComunicateService;
using pBot.Model.DataStructures;
using pBot.Model.Functions.Checkers._4pChecker;
using pBot.Model.Functions.HighLevel;

namespace pBot.Model.Facades
{
    public class P4Adapter : IProgrammingSitesAdapter
    {
        public IXmpp Xmpp { get; set; }
        public Checker4P Checker4P { get; set; }

        public Repeater P4Repeater { get; set; }
        private CachedResponse<string, string> Cache = new CachedResponse<string, string>();

        public P4Adapter()
        {
        }

        public string HotPost(string tag)
        {
            return Checker4P.GetLastMessagesByTag(tag);
        }

        public string HotPostsStream(string tag)
        {
            return P4Repeater.Add(tag, 10, () => Checker4P.GetLastMessagesByTag(tag,false));
        }

        public string HotPostsStreamStop(string tag)
        {
            return P4Repeater.RemoveRequest(tag);
        }

        public string NewThreads(string forumName)
        {
            return Checker4P.GetLastPostAtCategory(forumName);
        }

        public string NewThreadsStream(string forumName)
        {
            return P4Repeater.Add(forumName + "Thread", 10, () => Checker4P.GetLastPostAtCategory(forumName));
        }

        public string NewThreadsStreamStop(string forumName)
        {
            return P4Repeater.RemoveRequest(forumName + "Thread");
        }
    }
}
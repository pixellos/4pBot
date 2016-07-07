using pBot.Model.ComunicateService;
using pBot.Model.Functions.Checkers.SOChecker;
using pBot.Model.Functions.HighLevel;

namespace pBot.Dependencies
{
    class SoFacade : IProgrammingSitesFacade
    {
        public IXmpp Xmpp { get; set; }
        public CheckerSO CheckerSo { get; set; }
        public Repeater Repeater { get; set; }

        public string HotPost(string tag)
        {
            return CheckerSo.CheckNewestByTag(tag);
        }

        public string HotPostsStream(string tag)
        {
            return Repeater.Add(10,tag, () => CheckerSo.CheckNewestByTag(tag));
        }

        public string HotPostsStreamStop(string tag)
        {
            return Repeater.RemoveRequest(tag);
        }

        private string NotYetImplemented = "Not implemented yet!";
        public string NewThreads(string forumName)
        {
            return NotYetImplemented;
        }

        public string NewThreadsStream(string forumName)
        {
            return NotYetImplemented;
        }

        public string NewThreadsStreamStop(string forumName)
        {
            return NotYetImplemented;
        }
    }
}
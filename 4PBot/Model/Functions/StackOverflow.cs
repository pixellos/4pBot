using CoreBot;
using _4PBot.Model.Functions;
using _4PBot.Model.Functions.HighLevel;
using _4PBot.Model.Functions.StackOverflow;
using System;

namespace _4PBot.Model.Facades
{
    public class StackOverflow : IProgrammingSitesAdapter, ICommand
    {
        private static readonly string NotYetImplemented = "Not implemented yet!";
        public Actions Actions => this.ConstructStandardProgrammingSitesQuotations("SO");
        private Checker Checker { get; }
        private Repeater Repeater { get; }

        public StackOverflow(Repeater repeater, Checker checker)
        {
            this.Checker = checker;
            this.Repeater = repeater;
        }

        public string HotPost(string tag)
        {
            return this.Checker.CheckNewestByTag(tag);
        }

        public string HotPostsStream(string tag)
        {
            return this.Repeater.Add(10, tag, () => this.Checker.CheckNewestByTag(tag));
        }

        public string HotPostsStreamStop(string tag)
        {
            return this.Repeater.RemoveRequest(tag);
        }

        public string NewThreads(string forumName)
        {
            return StackOverflow.NotYetImplemented;
        }

        public string NewThreadsStream(string forumName)
        {
            return StackOverflow.NotYetImplemented;
        }

        public string NewThreadsStreamStop(string forumName)
        {
            return StackOverflow.NotYetImplemented;
        }

    }
}
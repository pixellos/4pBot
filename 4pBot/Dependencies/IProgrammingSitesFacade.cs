namespace pBot.Dependencies
{
    public interface IProgrammingSitesFacade
    {
        string HotPost(string tag);
        string HotPostsStream(string tag);
        string HotPostsStreamStop(string tag);

        string NewThreads(string forumName);
        string NewThreadsStream(string forumName);
        string NewThreadsStreamStop(string forumName);
    }
}
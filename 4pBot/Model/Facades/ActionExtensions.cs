using CoreBot;
using CoreBot.Mask;

namespace _4PBot.Model.Facades
{
    internal static class ActionExtensions
    {
        enum Words
        {
            Tag,
            Repeat,
            Dont,
            Java,
        }

        public static Actions ConstructStandardProgrammingSitesQuotations(this IProgrammingSitesAdapter adapter, string forumPrefix)
        {
            var actions = new Actions
            {
                [Builder.Bot()
                    .Requried(forumPrefix)
                    .ThenString(nameof(Words.Tag), nameof(Words.Java))
                    .Requried(nameof(Words.Repeat))
                    .Requried(nameof(Words.Tag)).End()
                ] = result => adapter.HotPostsStream(result[nameof(Words.Tag)]),
                [Builder.Bot()
                    .Requried(nameof(Words.Dont))
                    .Requried(forumPrefix)
                    .ThenString(nameof(Words.Tag), nameof(Words.Java))
                    .Requried(nameof(Words.Repeat))
                    .Requried(nameof(Words.Tag))
                    .End()
                ] = result => adapter.HotPostsStreamStop(result[nameof(Words.Tag)]),
                [Builder.Bot()
                    .Requried(forumPrefix)
                    .ThenString(nameof(Words.Tag), nameof(Words.Java))
                    .End()
                ] = result => adapter.HotPost(result[nameof(Words.Tag)])
            };
            return actions;
        }
    }
}

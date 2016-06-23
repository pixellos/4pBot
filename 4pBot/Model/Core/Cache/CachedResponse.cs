using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace pBot.Model.Core
{
    public class CachedResponse
    {
        Dictionary<Command,string> Cache  = new Dictionary<Command, string>();

        public ImmutableDictionary<Command, string> ReadOnlyCache => Cache.ToImmutableDictionary();

        public bool ContainsCommand(Command command)
        {
            return Cache.Any(x => x.Key == command);
        }

        public bool IsResponseUnique(Command command,string response)
        {
            return ! Cache.Single(x=>x.Key == command).Value.Equals( response);
        }

        public void SetLastResponse(Command command, string response)
        {
            Cache[command] = response;
        }

        public void DoWhenResponseIsNotLikeLastResponse(Command command, string response, Action<string> action)
        {
            if (! ContainsCommand(command))
            {
                InitializeCommand(command);
            }

            if (IsResponseUnique(command, response))
            {
                SetLastResponse(command, response);
                action(response);
            }
        }

        public string GetCacheValue(Command command)
        {
            return Cache.SingleOrDefault(x => x.Key == command).Value;
        }

        void InitializeCommand(Command command)
        {
            Cache.Add(command, "");
        }

        public void Remove(Command command)
        {
            Cache.Remove(Cache.First(x => x.Key == command).Key);
        }
    }
}
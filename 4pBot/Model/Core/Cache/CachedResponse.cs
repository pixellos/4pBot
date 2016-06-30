using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace pBot.Model.Core.Cache
{
    public class CachedResponse
    {
        private readonly Dictionary<Data.Command, string> Cache = new Dictionary<Data.Command, string>();

        public ImmutableDictionary<Data.Command, string> ReadOnlyCache => Cache.ToImmutableDictionary();

        public bool ContainsCommand(Data.Command command)
        {
            return Cache.Any(x => x.Key == command);
        }

        public bool IsResponseUnique(Data.Command command, string response)
        {
            return !Cache.Single(x => x.Key == command).Value.Equals(response);
        }

        public void SetLastResponse(Data.Command command, string response)
        {
            Cache[command] = response;
        }

        public void DoWhenResponseIsNotLikeLastResponse(Data.Command command, string response, Action<string> action)
        {
            if (!ContainsCommand(command))
            {
                InitializeCommand(command);
            }

            if (IsResponseUnique(command, response))
            {
                SetLastResponse(command, response);
                action(response);
            }
        }

        public string GetCacheValue(Data.Command command)
        {
            return Cache.SingleOrDefault(x => x.Key == command).Value;
        }

        private void InitializeCommand(Data.Command command)
        {
            Cache.Add(command, "");
        }

        public void Remove(Data.Command command)
        {
            Cache.Remove(Cache.First(x => x.Key == command).Key);
        }
    }
}
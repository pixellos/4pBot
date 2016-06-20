﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace pBot.Model.Core
{
    public class CachedResponse
    {
        public Dictionary<Command,string> Cache  = new Dictionary<Command, string>();

        public bool IsAnyInCacheMatchingCommand(Command command)
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
            if (! IsAnyInCacheMatchingCommand(command))
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

        public void InitializeCommand(Command command)
        {
            Cache.Add(command, "");
        }

        public void Remove(Command command)
        {
            Cache.Remove(Cache.First(x => x.Key == command).Key);
        }
    }
}
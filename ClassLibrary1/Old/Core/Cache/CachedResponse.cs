using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BotOrder.Old.Core.Cache
{

    public interface ICache<TBase,TResult>
    {
        ImmutableDictionary<TBase, TResult> ReadOnlyCache { get; }
        bool ContainsKey(TBase TBase);
        bool IsResponseUnique(TBase TBase, TResult TResult);
        void SetLastResponse(TBase TBase, TResult TResult);
        void DoWhenResponseIsNotLikeLastResponse(TBase TBase, TResult TResult, Action<TResult> action);
        TResult GetCacheValue(TBase TBase);
        void InitializeKey(TBase TBase);
        void Remove(TBase TBase);
    }

    public class CachedResponse<TBase,TResult> : ICache<TBase, TResult> where TBase : class 
    {
        private readonly Dictionary<TBase, TResult> Cache = new Dictionary<TBase, TResult>();

        public ImmutableDictionary<TBase, TResult> ReadOnlyCache => Cache.ToImmutableDictionary();

        public bool ContainsKey(TBase TBase)
        {
            return Cache.Any(x => x.Key == TBase);
        }

        public bool IsResponseUnique(TBase TBase, TResult TResult)
        {
            return !Cache.Single(x => x.Key == TBase).Value.Equals(TResult);
        }

        public void SetLastResponse(TBase TBase, TResult TResult)
        {
            Cache[TBase] = TResult;
        }

        public void DoWhenResponseIsNotLikeLastResponse(TBase TBase, TResult TResult, Action<TResult> action)
        {
            if (!ContainsKey(TBase))
            {
                InitializeKey(TBase);
            }

            if (IsResponseUnique(TBase, TResult))
            {
                SetLastResponse(TBase, TResult);
                action(TResult);
            }
        }

        public TResult GetCacheValue(TBase TBase)
        {
            return Cache.SingleOrDefault(x => x.Key == TBase).Value;
        }



        public void InitializeKey(TBase TBase)
        {
            Cache.Add(TBase, default(TResult));
        }

        public void Remove(TBase TBase)
        {
            Cache.Remove(Cache.First(x => x.Key == TBase).Key);
        }
    }
}
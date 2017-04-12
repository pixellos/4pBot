using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace _4PBot.Model.DataStructures
{
    public class CachedResponse<TBase, TResult> : ICache<TBase, TResult> where TBase : class
    {
        private readonly Dictionary<TBase, TResult> Cache = new Dictionary<TBase, TResult>();

        public ImmutableDictionary<TBase, TResult> ReadOnlyCache => this.Cache.ToImmutableDictionary();

        public bool ContainsKey(TBase TBase)
        {
            return this.Cache.Any(x => x.Key.Equals(TBase));
        }

        public bool IsResponseUnique(TBase TBase, TResult TResult)
        {
            return !this.Cache.Single(x => x.Key.Equals(TBase)).Value.Equals(TResult);
        }

        public void SetLastResponse(TBase TBase, TResult TResult)
        {
            this.Cache[TBase] = TResult;
        }

        public void DoWhenResponseDifferentThanPrevious(TBase TBase, TResult TResult, Action<TResult> action, TResult baseResult = default(TResult))
        {
            if (!this.ContainsKey(TBase))
            {
                this.InitializeKey(TBase,baseResult);
            }
            if (this.IsResponseUnique(TBase, TResult))
            {
                this.SetLastResponse(TBase, TResult);
                action(TResult);
            }
        }

        public TResult GetCacheValue(TBase TBase)
        {
            return this.Cache.SingleOrDefault(x => x.Key.Equals(TBase)).Value;
        }

        public void InitializeKey(TBase TBase,TResult initializer)
        {
            this.Cache.Add(TBase, initializer);
        }

        public void Remove(TBase TBase)
        {
            this.Cache.Remove(this.Cache.First(x => x.Key.Equals(TBase)).Key);
        }
    }
}
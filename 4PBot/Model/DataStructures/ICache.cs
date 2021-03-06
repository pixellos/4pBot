using System;
using System.Collections.Immutable;

namespace _4PBot.Model.DataStructures
{
    public interface ICache<TBase, TResult>
    {
        ImmutableDictionary<TBase, TResult> ReadOnlyCache { get; }
        bool ContainsKey(TBase TBase);
        bool IsResponseUnique(TBase TBase, TResult TResult);
        void SetLastResponse(TBase TBase, TResult TResult);
        void DoWhenResponseDifferentThanPrevious(TBase TBase, TResult TResult, Action<TResult> action,TResult baseTResult);
        TResult GetCacheValue(TBase TBase);
        void InitializeKey(TBase TBase, TResult initializer);
        void Remove(TBase TBase);
    }
}
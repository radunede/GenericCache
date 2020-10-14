using System;
namespace Cache
{
    public interface ICache
    {
        void Add(string key, object value, Action callback = null);
        object Get(string key);
        void SetCacheSize(int size);
        int Count { get; }
    }
}

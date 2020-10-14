using System;
using System.Collections.Concurrent;

namespace Cache
{
    public class Cache : ICache
    {
        public Cache(IDoubleLinkedList doubleLinkedList,int size = 100)
        {
            Size = size;
            _doubleLinkedList = doubleLinkedList;
            _cacheStore = new ConcurrentDictionary<string, object>();
        }

        private object _lock = new object();
        private int Size { get; set; }

        public int Count => (int)(_cacheStore?.Count);

        private readonly IDoubleLinkedList _doubleLinkedList;
        private readonly ConcurrentDictionary<string, object> _cacheStore;

        public void Add(string key, object value, Action callback = null)
        {
            lock (_lock)
            {
                if (_cacheStore.ContainsKey(key))
                {
                    var obj = _cacheStore[key];

                }
                else
                {
                    if (_cacheStore.Count == Size)
                    {
                        //we need to remove
                        string keyValue = _doubleLinkedList.Remove();
                        _cacheStore.TryRemove(keyValue, out _);

                        _cacheStore.TryAdd(key, value);                      
                        _doubleLinkedList.Insert(key, callback);
                    }
                    else
                    {
                        //insert as usual
                        _cacheStore.TryAdd(key, value);
                        _doubleLinkedList.Insert(key, callback);
                    }
                }
            }
        }

        public object Get(string key)
        {
            lock (_lock)
            {
                if (_cacheStore.ContainsKey(key))
                {
                    _doubleLinkedList.Insert(_doubleLinkedList.Remove(key));
                    return _cacheStore[key];
                }
                else
                {
                    return null;
                }
            }
        }

        public void SetCacheSize(int size)
        {
            lock (_lock)
            {
                Size = size;
                //TODO Remove each element untill size is trimmed.
                if (_cacheStore.Count > Size)
                {
                    while (_cacheStore.Count > Size)
                    {
                        var removableKey = _doubleLinkedList.Remove();
                        _cacheStore.TryRemove(removableKey, out _);
                    }
                }
            }
        }
    }
}

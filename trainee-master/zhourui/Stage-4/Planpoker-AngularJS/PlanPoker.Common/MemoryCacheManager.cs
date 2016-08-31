using System.Collections.Generic;

namespace PlanPoker.Common
{
    public class MemoryCacheManager : IMemoryCacheManager
    {
        public readonly Dictionary<string, object> Cache;

        public MemoryCacheManager()
        {
            Cache = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            Cache.Add(key, value);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public bool KeyExists(string key)
        {
            return Cache.ContainsKey(key);
        }
    }
}
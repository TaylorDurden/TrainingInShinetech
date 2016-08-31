using System.Collections.Generic;

namespace PlanPoker.Common
{
    public interface ICacheManager
    {
        void Add(string key, object value);
        void Remove(string key);
        T Get<T>(string key);
        bool KeyExists(string key);
    }
}
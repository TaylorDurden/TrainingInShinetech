using System.Collections.Generic;

namespace PlanPoker.Common
{
    public interface ICacheManger
    {
        void Add(string key, object value);
        void Remove(string key);
        T Get<T>(string key);
        bool KeyExist(string key);
    }
}
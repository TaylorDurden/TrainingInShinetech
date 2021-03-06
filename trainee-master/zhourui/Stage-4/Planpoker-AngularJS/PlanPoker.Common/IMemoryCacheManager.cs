namespace PlanPoker.Common
{
    public interface IMemoryCacheManager
    {
        void Add(string key, object value);
        void Remove(string key);
        T Get<T>(string key);
        bool KeyExists(string key);
    }
}
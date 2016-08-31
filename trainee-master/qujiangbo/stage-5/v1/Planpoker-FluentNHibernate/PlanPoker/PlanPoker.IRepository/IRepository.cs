using System.Collections.Generic;

namespace PlanPoker.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Create(T model);
        void Delete(int id);
        void Edit(T model);
        IList<T> Query();
        T Get(object id);
    }
}

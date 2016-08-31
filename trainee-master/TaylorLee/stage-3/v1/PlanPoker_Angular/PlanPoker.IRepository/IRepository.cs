using System.Linq;

namespace PlanPoker.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Create(T model);
        void Delete(int id);
        void Edit(T model);
        IQueryable<T> Query();
        T Get(int id);
    }
}

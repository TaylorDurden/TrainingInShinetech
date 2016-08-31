using System.Linq;

namespace BugManagement.DAL.IRepository
{
    public interface IRepositoryBase<T> where T : class
    {
        void Create(T model);

        void Delete(int id);

        IQueryable<T> Query();

        T Get(int id);

        void Edit(T model);
        
    }
}

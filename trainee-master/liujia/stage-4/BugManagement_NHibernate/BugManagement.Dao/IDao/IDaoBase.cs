using System.Collections.Generic;

namespace BugManagement.Dao.IDao
{
    public interface IDaoBase<T> where T : class
    {
        void Create(T model);

        void Delete(object id);

        IList<T> LoadAll();

        T Get(object id);

        void Edit(T model);
    }
}
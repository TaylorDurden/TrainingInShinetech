using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

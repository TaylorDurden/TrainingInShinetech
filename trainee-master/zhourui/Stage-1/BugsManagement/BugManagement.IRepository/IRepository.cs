using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BugManagement.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Ins(T model);
        void Del(int id);
        void Upd(T model);

        IEnumerable<T> Query(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = ""
            );
    }
}

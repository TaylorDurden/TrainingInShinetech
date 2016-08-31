using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlanPoker.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Create(T model);
        void Delete(int id);
        void Edit(T model);

        T Get(object id);

        T Load(object id);

        List<T> LoadAll();

        List<T> LoadAllWithPage(out long count, int pageIndex, int pageSize);

        IList<T> GetListForPage(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int pageIndex, int pageSize, out int total);
    }
}
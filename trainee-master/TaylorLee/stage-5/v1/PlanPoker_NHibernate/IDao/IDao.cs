using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlanPoker.IDao
{
    public interface IDao<T> where T : class
    {
        T Create(T entity);
        void Edit(T entity);
        void Delete(T entity);
        T Get(object id);
        IQueryable<T> Query();
        T Load(object id);
        IList<T> LoadAll();
        List<T> GetByIds(Expression<Func<T, bool>> whereLambda);
    }
}

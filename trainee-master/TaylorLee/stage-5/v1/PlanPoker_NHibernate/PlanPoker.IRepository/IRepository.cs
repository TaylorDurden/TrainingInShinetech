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
        IQueryable<T> Query();
        T Get(int id);
        T Merge(T model);
    }
}

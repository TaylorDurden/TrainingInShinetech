using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Castle.Core.Internal;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using PlanPoker.Data;

namespace PlanPoker.Repository
{
    public class RepositoryBase<T> where T : class
    {
        private readonly IDbFactory _dbFactory;

        public RepositoryBase(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public T Create(T model)
        {
            DbSet.Add(model);
            return model;
        }
        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
        public IQueryable<T> Query()
        {
            return DbSet.AsQueryable();
        }
        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public List<T> GetByIds(Expression<Func<T, bool>> whereLambda)
        {
            return DbSet.Where(whereLambda).ToList();
        }

        public void Edit(T model)
        {
            DataContext.Entry(model).State = EntityState.Modified;
        }

        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<T> DbSet => DataContext.Set<T>();
    }
}

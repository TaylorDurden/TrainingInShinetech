using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BugManagement.Data;

namespace BugManagement.Repository
{
    public class RepositoryBase<T> where T : class
    {
        private readonly IDbFactory _dbFactory;

        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<T> DbSet => DataContext.Set<T>();

        public RepositoryBase(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Ins(T model)
        {
            DbSet.Add(model);
        }

        public void Del(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Upd(T model)
        {
            DataContext.Entry(model).State = EntityState.Modified;
        }

        public IEnumerable<T> Query(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (
                var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}

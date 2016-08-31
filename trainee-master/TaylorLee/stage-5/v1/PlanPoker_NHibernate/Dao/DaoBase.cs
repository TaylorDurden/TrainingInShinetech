using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using PlanPoker.Data;

namespace PlanPoker.Dao
{
    public class DaoBase<T> where T : class
    {
        private readonly IDbFactory _dbFactory;
        private ISession SessionContext => _dbFactory.GetISessionContext();

        public DaoBase(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public T Create(T entity)
        {
            SessionContext.Save(entity);
            SessionContext.Flush();
            return entity;
        }

        public void Edit(T entity)
        {
            SessionContext.Update(entity);
            SessionContext.Flush();
        }

        public void Delete(int id)
        {
            SessionContext.Delete(Get(id));
            SessionContext.Flush();
        }

        public T Get(int id)
        {
            return SessionContext.Get<T>(id);
        }

        public T Load(object id)
        {
            return SessionContext.Load<T>(id);
        }

        public IQueryable<T> Query()
        {
            return SessionContext.Query<T>();
        }

        public IList<T> LoadAll()
        {
            return SessionContext.Query<T>().ToList();
        }

        public T Merge(T entity)
        {
            return SessionContext.Merge(entity);
        }
    }
}

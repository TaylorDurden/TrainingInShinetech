using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using PlanPoker.IRepository;
using PlanPoker.Repository.SessionHelper;

namespace PlanPoker.Repository
{
    public class NHiberanteRepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly ISessionProvider _sessionProvider;

        protected ISession Session => _sessionProvider.GetCurrentSession();

        public NHiberanteRepositoryBase(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public T Create(T model)
        {
            return (T) Session.Save(model);
        }

        public void Delete(int id)
        {
            Session.Delete(Get(id));
        }

        public void Edit(T model)
        {
            Session.Update(model);
        }

        public T Get(object id)
        {
            return Session.Get<T>(id);
        }

        public T Load(object id)
        {
            return Get(id);
        }

        public IList<T> GetListForPage(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int pageIndex,
            int pageSize, out int total)
        {
            var query = Session.QueryOver<T>().Where(where).OrderBy(order).Desc;
            total = query.List<T>().Count;

            return query.Skip((pageIndex - 1)*pageSize).Take(pageSize).List<T>();
        }


        public List<T> LoadAll()
        {
            return Session.Query<T>().ToList();
        }

        public List<T> LoadAllWithPage(out long count, int pageIndex, int pageSize)
        {
            var result = Session.QueryOver<T>();
            count = result.RowCount();

            return result.Skip((pageIndex - 1)*pageSize).Take(pageSize).List().ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using NHibernate;
using NHibernate.Linq;

namespace BugManagement.Dao.Dao
{
    public class DaoBase<T> where T : class
    {
        private readonly ISessionProvider _sessionProvider;
        protected ISession Session => _sessionProvider.GetCurrentSession();

        public DaoBase(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public void Create(T model)
        {
            Session.Save(model);
        }

        public void Delete(object id)
        {
            var model = Session.Get<T>(id);
            Session.Delete(model);
        }

        public IList<T> LoadAll()
        {
            return Session.Query<T>().ToList();
        }

        public T Get(object id)
        {
            return Session.Get<T>(id);
        }

        public void Edit(T model)
        {
            Session.Update( model);
        }
    }
}
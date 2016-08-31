using System.Linq;
using PlanPoker.IRepository;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;

namespace PlanPoker.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly ISession _session = NHibernateHelper.OpenSession();
        public void Create(T model)
        {
            _session.Save(model);
            _session.Flush();
        }

        public void Delete(int id)
        {
            _session.Delete(Get(id));
            _session.Flush();
        }

        public void Edit(T model)
        {
            _session.Update(model);
            _session.Flush();
        }

        public T Get(object id)
        {
            return _session.Get<T>(id);
        }

        public IList<T> Query()
        {
            return _session.Query<T>().ToList();
        }
    }
}

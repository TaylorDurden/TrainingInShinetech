using System.Linq;
using NHibernate;
using NHibernate.Linq;
using PlanPoker.IRepository;
using RepositoryNhibernate.Dal;

namespace RepositoryNhibernate
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly IFluentNHibernateHelper _fluentNHibernateHelper;
        public ISession Session => _fluentNHibernateHelper.GetSession();
        public RepositoryBase(IFluentNHibernateHelper fluentNHibernateHelper)
        {
            _fluentNHibernateHelper = fluentNHibernateHelper;
        }

        public void Create(T model)
        {
            Session.Save(model);
        }

        public void Delete(int id)
        {
            var model = Session.Get<T>(id);
            Session.Delete(model);
        }

        public void Edit(T model)
        {
            Session.Update(model);
        }

        public IQueryable<T> Query()
        {
            return Session.Query<T>();
        }

        public T Get(int id)
        {
            var model = Session.Get<T>(id);

            return model;
        }
    }
}

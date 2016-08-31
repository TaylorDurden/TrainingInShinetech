using Castle.Windsor;
using NHibernate;

namespace RepositoryNhibernate.Dal
{
    public class FluentNHibernateHelper : IFluentNHibernateHelper
    {
        private readonly IWindsorContainer _container;

        public FluentNHibernateHelper(IWindsorContainer container)
        {
            _container = container;
        }

        public ISession GetSession()
        {
            return _container.Resolve<ISession>();
        }
    }
}

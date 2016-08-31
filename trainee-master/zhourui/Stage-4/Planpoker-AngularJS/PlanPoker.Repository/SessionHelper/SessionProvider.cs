using Castle.Windsor;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace PlanPoker.Repository.SessionHelper
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IWindsorContainer _container;

        public SessionProvider(IWindsorContainer container)
        {
            _container = container;
        }

        public ISession GetCurrentSession()
        {
            return _container.Resolve<ISession>();
        }
    }
}

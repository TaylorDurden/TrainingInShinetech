
using BugManagement.Dao.IDao;
using Castle.Windsor;
using NHibernate;

namespace BugManagement.Dao.Dao
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

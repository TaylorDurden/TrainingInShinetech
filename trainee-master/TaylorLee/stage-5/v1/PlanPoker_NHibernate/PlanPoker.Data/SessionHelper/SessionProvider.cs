using Castle.Windsor;
using NHibernate;

namespace PlanPoker.Data.SessionHelper
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
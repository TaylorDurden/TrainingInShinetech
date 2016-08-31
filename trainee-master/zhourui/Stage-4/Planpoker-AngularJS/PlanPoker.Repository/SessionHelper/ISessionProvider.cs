using NHibernate;

namespace PlanPoker.Repository.SessionHelper
{
    public interface ISessionProvider
    {
        ISession GetCurrentSession();
    }
}

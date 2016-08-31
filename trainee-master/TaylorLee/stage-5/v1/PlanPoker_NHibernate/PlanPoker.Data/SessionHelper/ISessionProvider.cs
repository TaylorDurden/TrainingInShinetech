using NHibernate;

namespace PlanPoker.Data.SessionHelper
{
    public interface ISessionProvider
    {
        ISession GetCurrentSession();
    }
}
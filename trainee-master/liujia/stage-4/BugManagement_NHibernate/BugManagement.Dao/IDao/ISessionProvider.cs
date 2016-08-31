
using NHibernate;

namespace BugManagement.Dao.IDao
{
    public interface ISessionProvider
    {
        ISession GetCurrentSession();
    }
}

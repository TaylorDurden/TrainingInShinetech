using System.Data.Entity;
using NHibernate;

namespace PlanPoker.Data
{
    public interface IDbFactory
    {
        DbContext GetContext();

        ISession GetISessionContext();
    }
}

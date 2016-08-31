using System.Data.Entity;

namespace PlanPoker.Data
{
    public interface IDbFactory
    {
        DbContext GetContext();
    }
}

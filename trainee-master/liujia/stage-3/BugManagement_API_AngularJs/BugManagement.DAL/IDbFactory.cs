using System.Data.Entity;

namespace BugManagement.DAL
{
    public interface IDbFactory
    {
        DbContext GetContext();
    }
}

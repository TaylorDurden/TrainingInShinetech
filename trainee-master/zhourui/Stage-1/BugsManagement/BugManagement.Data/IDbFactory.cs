using System.Data.Entity;

namespace BugManagement.Data
{
    public interface IDbFactory
    {
        DbContext GetContext();
    }
}

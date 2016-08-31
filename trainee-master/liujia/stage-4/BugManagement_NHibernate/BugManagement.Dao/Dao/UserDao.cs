using BugManagement.Dao.IDao;
using BugManagement.Data.Models.Mappings;

namespace BugManagement.Dao.Dao
{
    public class UserDao : DaoBase<User>, IUserDao
    {
        public UserDao(ISessionProvider sessionProvider):base(sessionProvider)
        {
        }
    }
}
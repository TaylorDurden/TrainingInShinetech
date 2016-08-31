using BugManagement.Dao.IDao;
using BugManagement.Data.Models.Mappings;

namespace BugManagement.Dao.Dao
{
    public class BugTypeDao : DaoBase<BugType>, IBugTypeDao
    {
        public BugTypeDao(ISessionProvider sessionProvider) :base(sessionProvider)
        {
        }
    }
}
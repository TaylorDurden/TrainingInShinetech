using BugManagement.Dao.IDao;
using BugManagement.Data.Models.Mappings;
using NHibernate;

namespace BugManagement.Dao.Dao
{
    public class DeveloperDao : DaoBase<Developer>, IDeveloperDao
    {
        public DeveloperDao(ISessionProvider sessionProvider) :base(sessionProvider)
        {
        }

    }
}
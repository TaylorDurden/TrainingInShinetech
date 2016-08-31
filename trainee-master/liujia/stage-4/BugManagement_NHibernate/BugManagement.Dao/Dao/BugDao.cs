using System.Collections.Generic;
using BugManagement.Dao.IDao;
using BugManagement.Data.Models.Mappings;
using NHibernate;

namespace BugManagement.Dao.Dao
{
    public class BugDao : DaoBase<Bug>, IBugDao
    {
        public BugDao(ISessionProvider sessionProvider):base(sessionProvider)
        {
        }

        public IList<Bug> GetBugsByCondition(string condition)
        {
            var bugs =
                    Session.CreateQuery("from Bug c where c.BugId='" + condition + "' or c.Title='" + condition + "'")
                        .List<Bug>();
            return bugs;
        }
    }
}
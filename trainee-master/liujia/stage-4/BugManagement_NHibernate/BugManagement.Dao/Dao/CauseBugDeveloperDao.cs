using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Data.Models.Mappings;
using NHibernate.Linq;

namespace BugManagement.Dao.Dao
{
    public class CauseBugDeveloperDao : DaoBase<CauseBugDeveloper>, ICauseBugDeveloperDao
    {

        public CauseBugDeveloperDao(ISessionProvider sessionProvider) :base(sessionProvider)
        {
        }
        
        public IList<CauseBugDeveloper> GetCauseBugDevelopersByBugId(int bugId)
        {
            var causeBugDevelopers = Session.Query<CauseBugDeveloper>().Where(n=>n.Bug.BugId == bugId).ToList();
            //Session.CreateQuery("from CauseBugDeveloper c where c.BugId=" + bugId + "")
            //            .List<CauseBugDeveloper>();
            return causeBugDevelopers;
        }
    }
}
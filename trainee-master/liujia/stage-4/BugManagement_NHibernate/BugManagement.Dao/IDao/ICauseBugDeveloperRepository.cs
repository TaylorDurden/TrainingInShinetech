using System.Collections.Generic;
using BugManagement.Data.Models.Mappings;

namespace BugManagement.Dao.IDao
{
    public interface ICauseBugDeveloperDao : IDaoBase<CauseBugDeveloper>
    {
        IList<CauseBugDeveloper> GetCauseBugDevelopersByBugId(int bugId);
    }
}
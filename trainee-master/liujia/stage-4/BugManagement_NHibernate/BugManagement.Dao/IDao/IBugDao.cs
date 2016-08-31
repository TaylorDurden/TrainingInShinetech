using System.Collections.Generic;
using BugManagement.Data.Models.Mappings;

namespace BugManagement.Dao.IDao
{
    public interface IBugDao : IDaoBase<Bug>
    {
        IList<Bug> GetBugsByCondition(string condition);
    }
}
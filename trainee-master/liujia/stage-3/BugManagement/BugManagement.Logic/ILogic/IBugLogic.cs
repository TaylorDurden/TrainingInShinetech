using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Logic.ILogic
{
    public interface IBugLogic
    {
        void Create(Bug model,string strDevelopers,string strDocuments);

        void Edit(Bug model, string strDevelopers, string strDocuments);
        
        void Delete(int id);
        
        Bug Get(int id);
        
        IEnumerable<Bug> GetAll();
        
        IEnumerable<Bug> GetBugByWhereCondition(string whereCondition);
        
        IEnumerable<Bug> GetBugBywhereConditionAndStatus(string whereCondition, string status);
        void UpdateBugStatus(int bugId, string stauts);
    }
}

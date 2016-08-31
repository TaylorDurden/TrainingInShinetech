using System.Collections.Generic;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ILogic
{
    public interface IBugLogic
    {
        void Create(BugLogicModel model,string strDevelopers,string strDocuments);

        void Edit(BugLogicModel model, string strDevelopers, string strDocuments);
        
        void Delete(int id);

        BugLogicModel Get(int id);

        List<BugLogicModel> GetAll();

        List<BugLogicModel> GetBugByWhereCondition(string whereCondition);
        
        void UpdateBugStatus(int bugId, string stauts);
    }
}

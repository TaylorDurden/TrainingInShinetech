using System.Collections.Generic;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ILogic
{
    public interface ICauseBugDeveloperLogic
    {
        void Create(CauseBugDeveloperLogicModel model);

        void Edit(CauseBugDeveloperLogicModel model);
        
        void Delete(int id);

        CauseBugDeveloperLogicModel Get(int id);
        
        List<CauseBugDeveloperLogicModel> GetAll();
        
        List<CauseBugDeveloperLogicModel> GetCauseBugDeveloperByBugId(int bugId);        
    }
}

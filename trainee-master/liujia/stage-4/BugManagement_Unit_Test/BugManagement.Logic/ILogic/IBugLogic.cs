using System.Collections.Generic;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ILogic
{
    public interface IBugLogic
    {
        void Create(BugLogicModel model,List<DeveloperLogicModel> developerLogicModels,string strDocuments);
        void Edit(BugLogicModel model, List<DeveloperLogicModel> developerLogicModels, string strDocuments);
        void Delete(int id);
        BugLogicModel Get(int id);
        List<BugLogicModel> GetAll();
        List<BugLogicModel> GetBugLogicModelsBySerchCondition(string serchCondition);
        int GetPageCountByCondition(string serchCondition);
        List<BugLogicModel> GetBugLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count);
        void UpdateBugStatus(int bugId, string stauts);
    }
}

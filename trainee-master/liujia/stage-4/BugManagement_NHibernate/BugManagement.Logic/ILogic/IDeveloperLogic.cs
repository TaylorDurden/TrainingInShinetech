using System.Collections.Generic;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ILogic
{
    public interface IDeveloperLogic
    {
        void Create(DeveloperLogicModel model);
        void Edit(DeveloperLogicModel model);
        void Delete(int id);
        DeveloperLogicModel Get(int id);
        List<DeveloperLogicModel> GetAll();
        int GetPageCountByCondition(string serchCondition);
        List<DeveloperLogicModel> GetDeveloperLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count);
    }
}

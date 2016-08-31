using System.Collections.Generic;
using PlanPoker.ILogic.Models;

//using PlanPoker.Data.Models;

namespace PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        ProjectLogicModel Create(ProjectLogicModel logicModel);
        ProjectLogicModel Edit(ProjectLogicModel logicModel);
        void Delete(int id);
        ProjectLogicModel Get(int id);
        IEnumerable<ProjectLogicModel> GetAll();
        ProjectLogicModel Get(string name);
        bool CheckIfProjectnameExists(string projectName);
    }
}
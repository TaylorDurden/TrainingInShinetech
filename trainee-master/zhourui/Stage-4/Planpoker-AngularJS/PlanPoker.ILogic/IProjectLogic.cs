using System.Collections.Generic;
using PlanPoker.ILogic.Models;

namespace PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        ProjectLogicModel Create(ProjectLogicModel logicModel);
        void Delete(int id);
        ProjectLogicModel Edit(ProjectLogicModel logicModel);
        ProjectLogicModel Get(int id);
        IEnumerable<ProjectLogicModel> GetAll();
        ProjectLogicModel Get(string name);
        bool CheckIfProjectnameExists(string projectName);
    }
}
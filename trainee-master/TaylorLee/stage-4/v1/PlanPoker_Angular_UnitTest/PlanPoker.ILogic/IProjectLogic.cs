using System.Collections.Generic;
using PlanPoker.ILogic.Models;

namespace PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        ProjectLogicModel Create(ProjectLogicModel model);
        void Edit(ProjectLogicModel model);
        void Delete(int id);
        ProjectLogicModel Get(int id);
        IEnumerable<ProjectLogicModel> GetAll();
        IEnumerable<ProjectLogicModel> Get(string name);
        bool CheckExist(string projectName);
    }
}

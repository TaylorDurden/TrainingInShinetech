using System.Collections.Generic;
using PlanPoker.ILogic.Models;

namespace PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        void Create(ProjectLogicModel model);
        void Edit(ProjectLogicModel model);
        void Delete(int id);
        ProjectLogicModel Get(int id);
        List<ProjectLogicModel> GetAll();
        List<ProjectLogicModel> Get(string name);
        bool CheckExist(string projectName);
    }
}

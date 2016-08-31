using System.Collections.Generic;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.LogicModel;

namespace PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        void Create(ProjectLogicModel model);
        void Edit(ProjectLogicModel model);
        void Delete(int id);
        ProjectLogicModel Get(int id);
        IEnumerable<ProjectLogicModel> GetAll();
        IEnumerable<ProjectLogicModel> Get(string name);
        bool CheckExist(string projectName);
    }
}

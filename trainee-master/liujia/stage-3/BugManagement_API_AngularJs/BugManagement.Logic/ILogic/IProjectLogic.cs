using System.Collections.Generic;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ILogic
{
    public interface IProjectLogic
    {
        void Create(ProjectLogicModel model);
        void Edit(ProjectLogicModel model);
        void Delete(int id);
        ProjectLogicModel Get(int id);
        List<ProjectLogicModel> GetAll();
        bool CheckExist(string projectName);
        List<ProjectLogicModel> GetProjectByWhereCondition(string whereCondition);
    }
}

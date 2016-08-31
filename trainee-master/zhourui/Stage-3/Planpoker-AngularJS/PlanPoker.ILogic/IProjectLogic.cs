using System.Collections.Generic;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        string Create(Project model);
        string Edit(Project model);
        string Delete(int id);
        Project Get(int id);
        IEnumerable<Project> GetAll();
        Project Get(string name);
        string GetGuidUrl(int id);
        bool CheckIfExist(string projectName);
    }
}
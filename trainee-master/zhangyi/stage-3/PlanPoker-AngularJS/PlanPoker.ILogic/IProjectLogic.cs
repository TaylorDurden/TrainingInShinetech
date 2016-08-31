using System.Collections.Generic;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        void Create(Project model);
        void Edit(Project model);
        void Delete(int id);
        Project Get(int id);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> Get(string name);
        bool CheckExist(string projectName);
    }
}

using System.Collections.Generic;
using BugManagement.Data.Models;

namespace BugManagement.ILogic
{
    public interface IProjectLogic
    {
        void Create(Project model);
        void Delete(int id);
        void Edit(Project model);
        IEnumerable<Project> GetAll();
    }
}

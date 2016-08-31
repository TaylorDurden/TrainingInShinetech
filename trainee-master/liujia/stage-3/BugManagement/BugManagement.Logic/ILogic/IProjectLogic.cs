using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Logic.ILogic
{
    public interface IProjectLogic
    {
        void Create(Project model);
        void Edit(Project model);
        void Delete(int id);
        Project Get(int id);
        IEnumerable<Project> GetAll();
        bool CheckExist(string projectName);
        IEnumerable<Project> GetProjectByWhereCondition(string whereCondition);
    }
}

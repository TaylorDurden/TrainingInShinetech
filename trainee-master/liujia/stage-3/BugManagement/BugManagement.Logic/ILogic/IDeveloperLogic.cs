using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Logic.ILogic
{
    public interface IDeveloperLogic
    {
        void Create(Developer model);
        void Edit(Developer model);
        void Delete(int id);
        Developer Get(int id);
        IEnumerable<Developer> GetAll();
        IEnumerable<Developer> GetDeveloperByWhereCondition(string whereCondition);
    }
}

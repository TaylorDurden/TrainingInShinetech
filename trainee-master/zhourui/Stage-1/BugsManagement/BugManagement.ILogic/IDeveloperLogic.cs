using System.Collections.Generic;
using BugManagement.Data.Models;

namespace BugManagement.ILogic
{
    public interface IDeveloperLogic
    {
        void Create(Developer model);
        void Delete(int id);
        void Edit(Developer model);
        IEnumerable<Developer> GetAll();
    }
}
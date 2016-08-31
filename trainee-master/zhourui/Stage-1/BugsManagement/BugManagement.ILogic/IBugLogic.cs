using System.Collections.Generic;
using BugManagement.Data.Models;

namespace BugManagement.ILogic
{
    public interface IBugLogic
    {
        void Create(Bug model);
        void Delete(int id);
        void Edit(Bug model);
        IEnumerable<Bug> GetAll();
    }
}
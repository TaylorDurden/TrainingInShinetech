using System.Collections.Generic;
using BugManagement.Data.Models;

namespace BugManagement.ILogic
{
    public interface IBugTypeLogic
    {
        void Create(BugType model);
        void Delete(int id);
        void Edit(BugType model);
        IEnumerable<BugType> GetAll();
    }
}
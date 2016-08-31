using System.Collections.Generic;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ILogic
{
    public interface IBugTypeLogic
    {
        List<BugTypeLogicModel> GetAll();        
    }
}

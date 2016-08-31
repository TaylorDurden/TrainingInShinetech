using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Logic.ILogic
{
    public interface IBugTypeLogic
    {
        void Create(BugType model);
        
        void Edit(BugType model);
        
        void Delete(int id);
        
        BugType Get(int id);
        
        IEnumerable<BugType> GetAll();        
    }
}

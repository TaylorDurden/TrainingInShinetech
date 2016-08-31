using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Logic.ILogic
{
    public interface ICauseBugDeveloperLogic
    {
        void Create(CauseBugDeveloper model);

        void Edit(CauseBugDeveloper model);
        
        void Delete(int id);
        
        CauseBugDeveloper Get(int id);
        
        IEnumerable<CauseBugDeveloper> GetAll();
        
        IEnumerable<CauseBugDeveloper> GetCauseBugDeveloperByBugId(int bugId);        
    }
}

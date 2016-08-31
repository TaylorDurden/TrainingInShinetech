using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugManagement.DAL.Model;

namespace BugManagement.Logic.ILogic
{
    public interface IDocumentLogic
    {
        IEnumerable<Document> GetDocumentByBugId(int bugId);
    }
}

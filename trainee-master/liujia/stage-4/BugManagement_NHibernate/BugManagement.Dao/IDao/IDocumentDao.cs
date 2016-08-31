using System.Collections.Generic;
using BugManagement.Data.Models.Mappings;

namespace BugManagement.Dao.IDao
{
    public interface IDocumentDao : IDaoBase<Document>
    {
        IList<Document> GetDocumentsByBugId(int bugId);
    }
}
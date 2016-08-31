using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Data.Models.Mappings;
using NHibernate;
using NHibernate.Linq;

namespace BugManagement.Dao.Dao
{
    public class DocumentDao : DaoBase<Document>, IDocumentDao
    {

        public DocumentDao(ISessionProvider sessionProvider) :base(sessionProvider)
        {
        }

        public IList<Document> GetDocumentsByBugId(int bugId)
        {
            var documents = Session.Query<Document>().Where(n => n.Bug.BugId == bugId).ToList();
            return documents;
        }
    }
}
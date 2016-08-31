using System.Data.Entity;
using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;

namespace BugManagement.DAL.Repository
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        public IDbSet<Document> DbSet => DataContext.Set<Document>();

        public DocumentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}

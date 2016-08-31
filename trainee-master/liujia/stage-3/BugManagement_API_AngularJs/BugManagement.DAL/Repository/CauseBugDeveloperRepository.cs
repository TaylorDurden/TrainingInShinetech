using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using System.Data.Entity;

namespace BugManagement.DAL.Repository
{
    public class CauseBugDeveloperRepository : RepositoryBase<CauseBugDeveloper>, ICauseBugDeveloperRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        public IDbSet<CauseBugDeveloper> DbSet => DataContext.Set<CauseBugDeveloper>();

        public CauseBugDeveloperRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}

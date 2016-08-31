using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using System.Data.Entity;

namespace BugManagement.DAL.Repository
{
    public class DeveloperRepository : RepositoryBase<Developer>, IDeveloperRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        public IDbSet<Developer> DbSet => DataContext.Set<Developer>();

        public DeveloperRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}

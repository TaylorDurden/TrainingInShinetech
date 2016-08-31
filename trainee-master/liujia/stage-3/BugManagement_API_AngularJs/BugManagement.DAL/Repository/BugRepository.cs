using BugManagement.DAL.Model;
using System.Data.Entity;
using BugManagement.DAL.IRepository;

namespace BugManagement.DAL.Repository
{
    public class BugRepository : RepositoryBase<Bug>,IBugRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        public IDbSet<Bug> DbSet => DataContext.Set<Bug>();

        public BugRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }
    }
}

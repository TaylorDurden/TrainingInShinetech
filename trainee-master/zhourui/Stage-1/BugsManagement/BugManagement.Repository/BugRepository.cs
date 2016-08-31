using BugManagement.Data;
using BugManagement.Data.Models;
using BugManagement.IRepository;

namespace BugManagement.Repository
{
    public class BugRepository : RepositoryBase<Bug>, IBugRepository
    {
        private readonly IDbFactory _dbFactory;

        public BugRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}

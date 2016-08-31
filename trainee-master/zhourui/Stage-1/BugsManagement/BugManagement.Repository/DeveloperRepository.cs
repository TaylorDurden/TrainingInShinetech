using BugManagement.Data;
using BugManagement.Data.Models;
using BugManagement.IRepository;

namespace BugManagement.Repository
{
    public class DeveloperRepository : RepositoryBase<Developer>, IDeveloperRepository
    {
        private readonly IDbFactory _dbFactory;

        public DeveloperRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}

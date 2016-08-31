using BugManagement.Data;
using BugManagement.Data.Models;
using BugManagement.IRepository;

namespace BugManagement.Repository
{
    public class BugTypeRepository : RepositoryBase<BugType>, IBugTypeRepository
    {
        private readonly IDbFactory _dbFactory;

        public BugTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}

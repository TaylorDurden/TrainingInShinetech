using BugManagement.Data;
using BugManagement.Data.Models;
using BugManagement.IRepository;

namespace BugManagement.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IDbFactory _dbFactory;

        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
    }
}

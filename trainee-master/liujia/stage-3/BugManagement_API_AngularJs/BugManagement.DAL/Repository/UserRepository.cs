using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using System.Data.Entity;

namespace BugManagement.DAL.Repository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        public IDbSet<User> DbSet => DataContext.Set<User>();

        public UserRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }        
    }
}

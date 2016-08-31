using System.Data.Entity;
using System.Linq;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<User> DbSet => DataContext.Set<User>();

        public UserRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }

        public User Get(string userName)
        {
            return DbSet.FirstOrDefault(u => u.UserName.Equals(userName));
        }
        
    }
}

using System.Data.Entity;
using System.Linq;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Repository
{
    public class UserRepository : RepositoryBase<PlanPokerUser>, IUserRepository
    {
        private readonly IDbFactory _dbFactory;
        private DbContext DataContext => _dbFactory.GetContext();
        private IDbSet<PlanPokerUser> DbSet => DataContext.Set<PlanPokerUser>();

        public UserRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            _dbFactory = dbfactory;
        }

        public PlanPokerUser Get(string userName)
        {
            return DbSet.FirstOrDefault(u => u.UserName.Equals(userName));
        }
        
    }
}

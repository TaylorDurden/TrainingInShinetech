using System.Linq;
using NHibernate;
using NHibernate.Linq;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.IDao;
using PlanPoker.IRepository;

namespace PlanPoker.Dao
{
    public class UserDao : DaoBase<User>, IUserRepository
    {
        private readonly IDbFactory _dbFactory;
        private ISession SessionContext => _dbFactory.GetISessionContext();
        public UserDao(IDbFactory dbfactory, IDbFactory dbFactory) : base(dbfactory)
        {
            _dbFactory = dbFactory;
        }

        public User Get(string userName)
        {
            return SessionContext.Query<User>().FirstOrDefault(x => x.UserName == userName);
        }
    }
}

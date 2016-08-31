using System.Linq;
using NHibernate.Linq;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public User Get(string userName)
        {
            var session = NHibernateHelper.OpenSession();
            return session.Query<User>().FirstOrDefault(x => x.UserName == userName);
        }

    }
}

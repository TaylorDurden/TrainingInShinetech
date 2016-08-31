using System.Linq;
using NHibernate.Linq;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.SessionHelper;

namespace PlanPoker.Repository
{
    public class UserNHiberanteRepository : NHiberanteRepositoryBase<User>, IUserRepository
    {
        public User GetByName(string name)
        {
            return Session.Query<User>().FirstOrDefault(x => x.UserName == name);
        }

        public UserNHiberanteRepository(ISessionProvider sessionProvider) : base(sessionProvider)
        {
        }
    }
}
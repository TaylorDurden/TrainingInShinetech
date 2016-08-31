using System.Linq;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;
using RepositoryNhibernate.Dal;
using NHibernate;

namespace RepositoryNhibernate
{
    public class UserRepository : RepositoryBase<PlanPokerUser>, IUserRepository
    {
        public PlanPokerUser Get(string userName)
        {
            IQuery query = Session.CreateQuery("from PlanPokerUser");
            var users = query.List<PlanPokerUser>();
            var planPokerUser = users.FirstOrDefault(x => x.UserName == userName);

            return planPokerUser;
        }

        public UserRepository(IFluentNHibernateHelper fluentNHibernateHelper) : base(fluentNHibernateHelper)
        {
        }
    }
}

using PlanPoker.Data.ModelsNHibernate;

namespace PlanPoker.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string userName);
    }
}

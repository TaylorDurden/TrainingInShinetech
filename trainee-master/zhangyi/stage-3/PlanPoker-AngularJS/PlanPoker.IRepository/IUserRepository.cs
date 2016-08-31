using PlanPoker.Data.Models;

namespace PlanPoker.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string userName);
    }
}

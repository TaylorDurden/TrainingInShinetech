using PlanPoker.Data.Models;

namespace PlanPoker.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByName(string name);
    }
}
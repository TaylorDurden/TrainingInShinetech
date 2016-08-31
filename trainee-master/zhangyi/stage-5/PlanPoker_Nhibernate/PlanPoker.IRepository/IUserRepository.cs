using PlanPoker.Data.Models;

namespace PlanPoker.IRepository
{
    public interface IUserRepository : IRepository<PlanPokerUser>
    {
        PlanPokerUser Get(string userName);
    }
}

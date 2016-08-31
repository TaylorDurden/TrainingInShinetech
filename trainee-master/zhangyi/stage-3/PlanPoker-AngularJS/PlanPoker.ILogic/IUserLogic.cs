using System.Collections.Generic;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic
{
    public interface IUserLogic
    {
        int Create(User model);
        void Edit(User model);
        void Delete(int id);
        User Get(int id);
        int Login(string userName, string password);
        IEnumerable<User> GetAll();
        IEnumerable<User> QueryByName(string userName);
        string GetUserImage(string userName);
    }
}

using System.Collections.Generic;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic
{
    public interface IUserLogic
    {
        string Create(User model);
        void Edit(User model);
        void Delete(int id);
        User Get(int id);
        string Login(string userName, string password);
        IEnumerable<User> GetAll();
        IEnumerable<User> QueryByName(string userName);
        string GetUserImage(string userName);
    }
}

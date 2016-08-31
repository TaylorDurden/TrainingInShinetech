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
        User Get(string userName);
        string Login(string userName, string password);
        IEnumerable<User> GetAll();
        string GetUserImage(string userName);
        bool CheckIfExist(string userName);
    }
}
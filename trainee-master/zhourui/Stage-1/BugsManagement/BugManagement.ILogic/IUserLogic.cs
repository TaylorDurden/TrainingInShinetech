using System.Collections.Generic;
using BugManagement.Data.Models;

namespace BugManagement.ILogic
{
    public interface IUserLogic
    {
        void Create(User model);
        void Delete(int id);
        void Update(User model);
        bool Login(string email, string password, out string message);
        User.UserType GetUserType(string email);
        IEnumerable<User> GetAll();
    }
}

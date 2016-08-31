using System.Collections.Generic;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ILogic
{
    public interface IUserLogic
    {
        void Create(UserLogicModel model);
        void Edit(UserLogicModel model);
        void Delete(int id);
        UserLogicModel Get(int id);
        void UpdateUserLastLoginTime(int userId);
        List<UserLogicModel> GetAll();
        UserLogicModel GetUserByEmailAndPassword(string email, string password);
        List<UserLogicModel> GetUserByWhereCondition(string whereCondition);
        bool CheckExist(string email);
    }
}

using System.Collections.Generic;
using PlanPoker.ILogic.Models;

namespace PlanPoker.ILogic
{
    public interface IUserLogic
    {
        string Create(UserLogicModel model);
        void Edit(UserLogicModel model);
        void Delete(int id);
        UserLogicModel Get(int id);
        List<UserLogicModel> GetByIds(int[] ids);
        string Login(string userName, string password);
        IEnumerable<UserLogicModel> GetAll();
        IEnumerable<UserLogicModel> QueryByName(string userName);
        string GetUserImage(string userName);
        bool CheckUserByName(string useName);
        bool ValidateUserPassword(string useName, string password);

    }
}

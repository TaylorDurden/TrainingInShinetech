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
        UserLogicModel Login(string userName, string password);
        List<UserLogicModel> GetAll();
        List<UserLogicModel> QueryByName(string userName);
        bool CheckExist(string userName);
        string GetUserImage(string userName);
    }
}

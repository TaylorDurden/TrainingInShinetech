using System.Collections.Generic;
using PlanPoker.ILogic.Models;

namespace PlanPoker.ILogic
{
    public interface IUserLogic
    {
        UserLogicModel Create(UserLogicModel logicModel);
        UserLogicModel Edit(UserLogicModel logicModel);
        void Delete(int id);
        UserLogicModel Get(int id);
        UserLogicModel Get(string userName);
        UserLogicModel Login(string userName, string password);
        IEnumerable<UserLogicModel> GetAll();
        string GetUserImage(string userName);
        bool CheckIfUsernameExists(string userName);
        bool CheckIfEmailExists(string email);
        bool CheckIfUserNewEmailExists(string userName, string email);
    }
}
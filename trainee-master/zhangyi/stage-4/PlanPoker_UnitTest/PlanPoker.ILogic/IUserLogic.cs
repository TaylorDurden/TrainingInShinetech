using System.Collections.Generic;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.LogicModel;

namespace PlanPoker.ILogic
{
    public interface IUserLogic
    {
        int Create(UserLogicModel model);
        void Edit(UserLogicModel model);
        void Delete(int id);
        UserLogicModel Get(int id);
        int Login(string userName, string password);
        IEnumerable<UserLogicModel> GetAll();
        IEnumerable<UserLogicModel> QueryByName(string userName);
        string GetUserImage(string userName);
        IEnumerable<UserLogicModel> GetUsersByUserIds(List<int> userIds);
    }
}

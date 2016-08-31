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
        List<UserLogicModel> GetAll();
        UserLogicModel LoginByEmailAndPassword(string email, string password);
        int GetPageCountByCondition(string serchCondition);
        List<UserLogicModel> GetUserLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize,int count);
        bool CheckExist(string email, string userId);
    }
}

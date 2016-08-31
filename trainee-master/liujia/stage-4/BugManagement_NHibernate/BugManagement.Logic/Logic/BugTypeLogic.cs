using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class BugTypeLogic: IBugTypeLogic
    {
        private readonly IBugTypeDao _bugTypeDao;

        public BugTypeLogic(IBugTypeDao bugTypeDao)
        {
            _bugTypeDao = bugTypeDao;
        }
        
        public List<BugTypeLogicModel> GetAll()
        {
            var model = _bugTypeDao.LoadAll().Where(n=>n.Status != "deleted").ToList();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToBugTypeLogicModel()).ToList();
        }
    }
}

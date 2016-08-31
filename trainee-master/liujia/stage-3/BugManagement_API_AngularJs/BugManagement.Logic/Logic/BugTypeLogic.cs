using BugManagement.DAL.IRepository;
using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class BugTypeLogic: IBugTypeLogic
    {
        private readonly IBugTypeRepository _bugTypeRepository;

        public BugTypeLogic(IBugTypeRepository bugTypeRepository)
        {
            _bugTypeRepository = bugTypeRepository;
        }
        
        public List<BugTypeLogicModel> GetAll()
        {
            var model = _bugTypeRepository.Query();
            return !model.Any()?null: model.ToList().Select(m => m.ConvertToBugTypeLogicModel()).ToList();
        }
    }
}

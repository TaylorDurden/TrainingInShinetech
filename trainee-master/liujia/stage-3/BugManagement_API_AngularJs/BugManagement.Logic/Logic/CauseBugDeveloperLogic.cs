using BugManagement.DAL.IRepository;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class CauseBugDeveloperLogic: ICauseBugDeveloperLogic
    {
        private readonly ICauseBugDeveloperRepository _causeBugDeveloperRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CauseBugDeveloperLogic(ICauseBugDeveloperRepository causeBugDeveloperRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _causeBugDeveloperRepository = causeBugDeveloperRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(CauseBugDeveloperLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _causeBugDeveloperRepository.Create(model.ConvertToCauseBugDeveloper());
                unitOfwork.Commit();
            }                
        }

        public void Edit(CauseBugDeveloperLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _causeBugDeveloperRepository.Edit(model.ConvertToCauseBugDeveloper());
                unitOfwork.Commit();
            }            
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _causeBugDeveloperRepository.Delete(id);
                unitOfwork.Commit();
            }            
        }

        public CauseBugDeveloperLogicModel Get(int id)
        {
            var causeBugDeveloper = _causeBugDeveloperRepository.Get(id);
            return causeBugDeveloper.ConvertToCauseBugDeveloperLogicModel();
        }

        public List<CauseBugDeveloperLogicModel> GetAll()
        {
            var model = _causeBugDeveloperRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToCauseBugDeveloperLogicModel()).ToList();
        }

        public List<CauseBugDeveloperLogicModel> GetCauseBugDeveloperByBugId(int bugId)
        {
            var model = _causeBugDeveloperRepository.Query().Where(n => n.BugId == bugId);
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToCauseBugDeveloperLogicModel()).ToList();
        }
    }
}

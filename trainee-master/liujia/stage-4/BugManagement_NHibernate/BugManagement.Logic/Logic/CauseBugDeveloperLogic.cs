using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Dao.UnitOfWork;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class CauseBugDeveloperLogic: ICauseBugDeveloperLogic
    {
        private readonly ICauseBugDeveloperDao _causeBugDeveloperDao;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IBugDao _bugDao;
        private readonly IDeveloperDao _developerDao;

        public CauseBugDeveloperLogic(ICauseBugDeveloperDao causeBugDeveloperDao, IUnitOfWorkFactory unitOfWorkFactory,IBugDao bugDao,IDeveloperDao developerDao)
        {
            _causeBugDeveloperDao = causeBugDeveloperDao;
            _unitOfWorkFactory = unitOfWorkFactory;
            _bugDao = bugDao;
            _developerDao = developerDao;
        }

        public void Create(CauseBugDeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var causeBugDeveloper = model.ConvertToCauseBugDeveloper();
                causeBugDeveloper.Bug = _bugDao.Get(model.BugId);
                causeBugDeveloper.Developer = _developerDao.Get(model.DeveloperId);
                _causeBugDeveloperDao.Create(causeBugDeveloper);
                unitWork.Commit();
            }
        }

        public void Edit(CauseBugDeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var causeBugDeveloperDb = _causeBugDeveloperDao.Get(model.Id);
                model.ConvertCauseBugDeveloperLogicModelToDbCauseBugDeveloper(causeBugDeveloperDb);
                _causeBugDeveloperDao.Edit(causeBugDeveloperDb);
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _causeBugDeveloperDao.Delete(id);
                unitWork.Commit();
            }
        }

        public CauseBugDeveloperLogicModel Get(int id)
        {
            var causeBugDeveloper = _causeBugDeveloperDao.Get(id);
            return causeBugDeveloper.ConvertToCauseBugDeveloperLogicModel();
        }

        public List<CauseBugDeveloperLogicModel> GetAll()
        {
            var model = _causeBugDeveloperDao.LoadAll();
            return !model.Any()
                ? null
                : model.ToList().Select(m => m.ConvertToCauseBugDeveloperLogicModel()).ToList();
        }

        public List<CauseBugDeveloperLogicModel> GetCauseBugDeveloperByBugId(int bugId)
        {
            var model = _causeBugDeveloperDao.LoadAll().Where(n => n.Bug.BugId == bugId).ToList();
            return !model.Any()
                ? null
                : model.ToList().Select(m => m.ConvertToCauseBugDeveloperLogicModel()).ToList();
        }
    }
}

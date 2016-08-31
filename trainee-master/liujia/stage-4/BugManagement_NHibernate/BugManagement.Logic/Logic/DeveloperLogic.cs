using System;
using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Dao.UnitOfWork;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private readonly IDeveloperDao _developerDao;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IUserDao _userDao;

        public DeveloperLogic(IDeveloperDao developerDao, IUnitOfWorkFactory unitOfWorkFactory,IUserDao userDao)
        {
            _developerDao = developerDao;
            _unitOfWorkFactory = unitOfWorkFactory;
            _userDao = userDao;
        }

        public void Create(DeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var developer = model.ConvertToDeveloper();
                developer.User = _userDao.Get(model.UserId);
                _developerDao.Create(developer);
                unitWork.Commit();
            }
        }

        public void Edit(DeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var developerDb = _developerDao.Get(model.DeveloperId);
                model.ConvertDeveloperLogicModelToDbDeveloper(developerDb);
                _developerDao.Edit(developerDb);
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerDao.Delete(id);
                unitWork.Commit();
            }
        }

        public DeveloperLogicModel Get(int id)
        {
            var developer = _developerDao.Get(id);
            return developer.ConvertToDeveloperLogicModel();
        }

        public List<DeveloperLogicModel> GetAll()
        {
            var model = _developerDao.LoadAll();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToDeveloperLogicModel()).ToList();
        }

        public int GetPageCountByCondition(string serchCondition)
        {
            return
                    _developerDao
                        .LoadAll()
                        .Count(n =>
                            string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                            n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                            n.Status.Contains(serchCondition));
        }

        public List<DeveloperLogicModel> GetDeveloperLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count)
        {
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            var model = _developerDao.LoadAll()
                .Where(n =>
                    string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                    n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                    n.Status.Contains(serchCondition))
                .OrderBy(n => n.DeveloperId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return model.Select(m => m.ConvertToDeveloperLogicModel()).ToList();
        }
    }
}

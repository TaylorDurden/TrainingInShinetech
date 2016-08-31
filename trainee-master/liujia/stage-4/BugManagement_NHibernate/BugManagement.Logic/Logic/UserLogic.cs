using BugManagement.Logic.ILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Dao.IDao;
using BugManagement.Dao.UnitOfWork;
using BugManagement.Data.Models.Mappings;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public UserLogic(IUserDao userDao, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userDao = userDao;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(UserLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var user = model.ConvertToUser();
                user.RegisterTime = DateTime.Now;
                if (user.Type == "Developer")
                {
                    var developer = new Developer
                    {
                        Email = user.Email,
                        FristName = user.FristName,
                        LastName = user.LastName,
                        Status = user.Status
                    };
                    user.Developers = new List<Developer> { developer };
                }

                _userDao.Create(user);
                unitWork.Commit();
            }
        }

        public void Edit(UserLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var userDb = _userDao.Get(model.UserId);
                model.ConvertUserLogicModelToDbUser(userDb);
                _userDao.Edit(userDb);
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var userDb = _userDao.Get(id);
                userDb.Status = "deleted";
                _userDao.Edit(userDb);
                unitWork.Commit();
            }
        }

        public UserLogicModel Get(int id)
        {
            var user = _userDao.Get(id);
            return user.ConvertToUserLogicModel();
        }

        public List<UserLogicModel> GetAll()
        {
            var model = _userDao.LoadAll().Where(n=>n.Status!= "deleted").ToList();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToUserLogicModel()).ToList();
        }

        public UserLogicModel LoginByEmailAndPassword(string email, string password)
        {
            var model = _userDao.LoadAll().Where(n => n.Status != "deleted").FirstOrDefault(n => n.Email == email && n.Password == password);
            if (model == null)
            {
                return null;
            }
            var user = _userDao.Get(model.UserId);
            user.LastLoginTime = DateTime.Now;
            _userDao.Edit(user);
            return model.ConvertToUserLogicModel();
        }

        public int GetPageCountByCondition(string serchCondition)
        {
            return
                    _userDao
                        .LoadAll().Where(n => n.Status != "deleted")
                        .Count(n => string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                                    n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                                    n.Status.Contains(serchCondition));
        }

        public List<UserLogicModel> GetUserLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count)
        {
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            var model = _userDao.LoadAll().Where(n => n.Status != "deleted")
                .Where(
                    n =>
                        string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                        n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                        n.Status.Contains(serchCondition))
                .OrderBy(n => n.UserId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return model.ConvertToUserLogicModels();
        }

        public bool CheckExist(string email, string userId)
        {
            if (_userDao.LoadAll().Any())
            {
                return _userDao
                    .LoadAll().Where(n => n.Status != "deleted")  
                    .Where(n => string.IsNullOrEmpty(userId) || n.UserId.ToString() != userId)
                    .Any(x => x.Email.Equals(email));
            }
            return false;
        }
    }
}

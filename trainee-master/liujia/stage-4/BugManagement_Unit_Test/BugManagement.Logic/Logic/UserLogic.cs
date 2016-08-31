using BugManagement.DAL.IRepository;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public UserLogic(IUserRepository userRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userRepository = userRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(UserLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Create(model.ConvertToUser());
                unitWork.Commit();
            }
        }

        public void Edit(UserLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Edit(model.ConvertToUser());
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Delete(id);
                unitWork.Commit();
            }
        }

        public UserLogicModel Get(int id)
        {
            var user = _userRepository.Get(id);
            return user.ConvertToUserLogicModel();
        }
        
        public List<UserLogicModel> GetAll()
        {
            var model = _userRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToUserLogicModel()).ToList();
        }

        public UserLogicModel LoginByEmailAndPassword(string email, string password)
        {
            var model = _userRepository.Query().FirstOrDefault(n => n.Email == email && n.Password == password);
            if (model == null)
            {
                return null;
            }
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var user = _userRepository.Get(model.UserId);
                user.LastLoginTime = DateTime.Now;
                _userRepository.Edit(user);
                unitWork.Commit();
            }
            return model.ConvertToUserLogicModel();
        }

        public int GetPageCountByCondition(string serchCondition)
        {
            return 
                _userRepository
                    .Query(
                        )
                    .Count(n => string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                            n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                            n.Status.Contains(serchCondition));
        }

        public List<UserLogicModel> GetUserLogicModelsByCondition(string serchCondition,int pageIndex,int pageSize,int count)
        {
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            var model = _userRepository.Query()
                .Where(
                    n =>
                        string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                        n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                        n.Status.Contains(serchCondition)).OrderBy(n=>n.UserId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return model.ConvertToUserLogicModels();
        }

        public bool CheckExist(string email, string userId)
        {
            if (_userRepository.Query().Any())
            {
                return _userRepository
                    .Query().Where(n => string.IsNullOrEmpty(userId) || n.UserId.ToString() != userId).Any(x => x.Email.Equals(email));
            }
            return false;
        }
    }
}

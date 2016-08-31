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

        public void UpdateUserLastLoginTime(int userId)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var user = _userRepository.Get(userId);
                user.LastLoginTime = DateTime.Now;
                _userRepository.Edit(user);
                unitWork.Commit();
            }
        }

        public List<UserLogicModel> GetAll()
        {
            var model = _userRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToUserLogicModel()).ToList();
        }

        public UserLogicModel GetUserByEmailAndPassword(string email, string password)
        {
            var model = _userRepository.Query().FirstOrDefault(n => n.Email == email && n.Password == password);
            return model?.ConvertToUserLogicModel();
        }

        public List<UserLogicModel> GetUserByWhereCondition(string whereCondition)
        {
            var model = _userRepository.Query().Where(n => string.IsNullOrEmpty(whereCondition) || n.FristName.Contains(whereCondition) || n.LastName.Contains(whereCondition) || n.Email.Contains(whereCondition) || n.Status.Contains(whereCondition));
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToUserLogicModel()).ToList();
        }

        public bool CheckExist(string email)
        {
            if (_userRepository.Query().Any())
            {
                return _userRepository
                    .Query().Any(x => x.Email.Equals(email));
            }
            return false;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userRepository = userRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public UserLogicModel Create(UserLogicModel userLogicModel)
        {
            if (userLogicModel == null) return null;

            if (_userRepository.Get(userLogicModel.UserName) != null)
            {
                userLogicModel.Message = "the username was registered, please select a new username to register.";
                return userLogicModel;
            }

            if (CheckIfEmailExists(userLogicModel.Email))
            {
                userLogicModel.Message = "the email was registered, please use another email to register.";
                return userLogicModel;
            }

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Create(userLogicModel.ConvertToUserEntity());
                unitOfwork.Commit();
            }

            return userLogicModel;
        }

        public UserLogicModel Edit(UserLogicModel userLogicModel)
        {
            if (userLogicModel == null) return null;

            if (CheckIfUserNewEmailExists(userLogicModel.UserName, userLogicModel.Email))
            {
                userLogicModel.Message = "the email was registered, please use another email to update.";
                return userLogicModel;
            }

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Edit(userLogicModel.ConvertToUserEntity());
                unitOfwork.Commit();
            }

            return userLogicModel;
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Delete(id);
                unitOfwork.Commit();
            }
        }

        public UserLogicModel Login(string userName, string password)
        {
            var user = _userRepository.Get(userName);

            if (user == null)
            {
                return new UserLogicModel {Message = "the username is not register."};
            }

            return user.Password.Equals(password)
                ? user.ConvertToUserLogicModel(string.Empty)
                : new UserLogicModel {Message = "the password is error."};
        }

        public IEnumerable<UserLogicModel> GetAll()
        {
            var users = _userRepository.Query().ToList();
            return users.Select(user => user.ConvertToUserLogicModel("")).ToList();
        }

        public UserLogicModel Get(int id)
        {
            return _userRepository.Get(id).ConvertToUserLogicModel(
                id > 0
                    ? ""
                    : "user id should greater than zero.");
        }

        public UserLogicModel Get(string username)
        {
            return _userRepository.Get(username).ConvertToUserLogicModel(
                !string.IsNullOrEmpty(username)
                    ? ""
                    : "user name cannot be empty.");
        }

        public string GetUserImage(string userName)
        {
            return _userRepository.Get(userName).Image;
        }

        public bool CheckIfUsernameExists(string userName)
        {
            return _userRepository.Query().Any(user => user.UserName == userName);
        }

        public bool CheckIfEmailExists(string email)
        {
            return _userRepository.Query().Any(user => user.Email == email);
        }

        public bool CheckIfUserNewEmailExists(string userName, string email)
        {
            return _userRepository.Query().Any(user => user.UserName != userName && user.Email == email);
        }
    }
}
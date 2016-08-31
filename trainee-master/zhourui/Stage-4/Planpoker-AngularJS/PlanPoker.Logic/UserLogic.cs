using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic
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

        public UserLogicModel Create(UserLogicModel userLogicModel)
        {
            if (userLogicModel == null) return null;

            if (_userRepository.GetByName(userLogicModel.UserName) != null)
            {
                userLogicModel.Message = "the username was registered, please select a new username to register.";
                userLogicModel.Status = false;
                return userLogicModel;
            }

            if (CheckIfEmailExists(userLogicModel.Email))
            {
                userLogicModel.Message = "the email was registered, please use another email to register.";
                userLogicModel.Status = false;
                return userLogicModel;
            }

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var user = _userRepository.Create(userLogicModel.ConvertToUserEntity()) as User;
                unitOfwork.Commit();

                return user.ConvertToUserLogicModel(string.Empty, true);
            }
        }

        public UserLogicModel Edit(UserLogicModel userLogicModel)
        {
            if (userLogicModel == null) return null;

            if (CheckIfUserNewEmailExists(userLogicModel.UserName, userLogicModel.Email))
            {
                userLogicModel.Message = "the email was registered, please use another email to update.";
                userLogicModel.Status = false;
                return userLogicModel;
            }

            var user = _userRepository.GetByName(userLogicModel.UserName);
            user.Password = userLogicModel.Password;
            user.Email = userLogicModel.Email;
            user.Image = userLogicModel.Image;

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Edit(user);
                unitOfwork.Commit();

                return user.ConvertToUserLogicModel(string.Empty, true);
            }
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
            var user = _userRepository.GetByName(userName);

            if (user == null)
            {
                return new UserLogicModel {Message = "the username is not register.", Status = false};
            }

            return user.Password.Equals(password)
                ? user.ConvertToUserLogicModel(string.Empty, true)
                : new UserLogicModel {Message = "the password is error.", Status = false};
        }

        public IEnumerable<UserLogicModel> GetAll()
        {
            return _userRepository.LoadAll()
                .Select(user => user.ConvertToUserLogicModel(string.Empty, true)).ToList();
        }

        public UserLogicModel Get(int id)
        {
            return id > 0
                ? _userRepository.Get(id).ConvertToUserLogicModel(string.Empty, true)
                : _userRepository.Get(id).ConvertToUserLogicModel("user id should greater than zero.", false);
        }

        public UserLogicModel Get(string username)
        {
            return !string.IsNullOrEmpty(username)
                ? _userRepository.GetByName(username).ConvertToUserLogicModel(string.Empty, true)
                : _userRepository.GetByName(username).ConvertToUserLogicModel("user name cannot be empty.", false);
        }

        public string GetUserImage(string userName)
        {
            return _userRepository.GetByName(userName).Image;
        }

        public bool CheckIfUsernameExists(string userName)
        {
            return _userRepository.LoadAll().Any(user => user.UserName == userName);
        }

        public bool CheckIfEmailExists(string email)
        {
            return _userRepository.LoadAll().Any(user => user.Email == email);
        }

        public bool CheckIfUserNewEmailExists(string userName, string email)
        {
            return _userRepository.LoadAll().Any(user => user.UserName != userName && user.Email == email);
        }
    }
}
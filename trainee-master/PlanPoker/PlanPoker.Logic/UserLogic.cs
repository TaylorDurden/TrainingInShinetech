using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
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

        public string Create(User model)
        {
            var message = "";
            if(!CheckIfExist(model.UserName))
            {
                using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
                {
                    _userRepository.Create(model);
                    unitOfwork.Commit();
                }
            }
            else
            {
                message = "the username was registered, please select a new username to register.";
            }
            return message;
        }

        public void Edit(User model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Edit(model);
                unitOfwork.Commit();
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

        public string Login(string userName, string password)
        {
            var user = _userRepository.Get(userName);

            if (user == null)
            {
                return "the username is not register";
            }
            if (user.Password.Equals(password))
            {
                return user.UserId.ToString();
            }

            return "the password error";
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.Query().ToList();
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        private bool CheckIfExist(string userName)
        {
            try
            {
                return _userRepository.Query().Where(x => x.UserName == userName).ToList().Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<User> QueryByName(string userName)
        {
            return CheckIfExist(userName)
                ? _userRepository.Query().Where(x => x.UserName == userName).ToList()
                : new List<User>();
        }

        public string GetUserImage(string userName)
        {
            return _userRepository.Get(userName).Image;
        }
    }
}

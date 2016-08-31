using System.Collections.Generic;
using System.Linq;
using BugManagement.Data.Models;
using BugManagement.ILogic;
using BugManagement.Infrastructure;
using BugManagement.IRepository;
using BugManagement.Repository.UnitOfWork;

namespace BugManagement.Logic
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

        public void Create(User model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                model.Password = Cryptography.Encrypt(model.Password);
                _userRepository.Ins(model);
                unitOfWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Del(id);
                unitOfWork.Commit();
            }
        }

        public void Update(User model)
        {
            using (var unitOfWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Upd(model);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.Query();
        }

        public User.UserType GetUserType(string email)
        {
            var user = _userRepository.Query(u => u.Email == email).FirstOrDefault();
            return user?.Type ?? User.UserType.Developer;
        }

        public bool Login(string email, string password, out string message)
        {
            message = "";

            var user = _userRepository.Query(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                message = "the Email is not registered";
                return false;
            }
            if (user.Password.Equals(password))
            {
                return true;
            }
            else
            {
                message = "the password is wrong";
                return false;
            }
        }
    }
}

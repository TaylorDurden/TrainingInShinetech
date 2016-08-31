using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using PlanPoker.Data.ModelsNHibernate;
using PlanPoker.ILogic;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;
using PlanPoker.ILogic.Models;

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

        public string Create(UserLogicModel model)
        {
            var message = "";
            var user = model.CreateConvert();
            var userModel = _userRepository.Get(model.UserName);

            if (userModel != null) return "the username was registered, please select a new username to register.";

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Create(user);
                unitOfwork.Commit();
            }

            return message;
        }

        public void Delete(int id)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Delete(id);
                unitOfwork.Commit();
            }
        }

        public void Edit(UserLogicModel model)
        {
            var user = model.EditConvert();
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Edit(user);
                unitOfwork.Commit();
            }
        }

        public UserLogicModel Login(string userName, string password)
        {
            var user = _userRepository.Get(userName);
            var userLogicModel = new User().LoginConvert();
            userLogicModel.Message = "the password error";
            userLogicModel.Status = false;

            if (user != null) return user.Password.Equals(password) ? user.LoginConvert() : userLogicModel;
            userLogicModel.Message = "the username is not register";

            return userLogicModel;
        }

        public List<UserLogicModel> GetAll()
        {
            return _userRepository.Query().ToList().GetAllConvert();
        }

        public UserLogicModel Get(int id)
        {
            return _userRepository.Get(id).GetConvert();            
        }

        public List<UserLogicModel> QueryByName(string userName)
        {
            var result= CheckExist(userName);
            return result ? _userRepository.Query().Where(x => x.UserName==userName).ToList().QueryByNameConvert()
                : new List<UserLogicModel>();
        }

        public string GetUserImage(string userName)
        {
            return _userRepository.Get(userName).Image;
        }

        public bool CheckExist(string userName)
        {
            return _userRepository.Query().Where(x => x.UserName == userName).ToList().Count > 0;
        }
    }
}

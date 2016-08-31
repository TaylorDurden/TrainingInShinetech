using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.IRepository;
using PlanPoker.Repository;
using PlanPoker.ILogic.Models;

namespace PlanPoker.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;           
        }

        public string Create(UserLogicModel model)
        {
            var message = "";
            var user = model.CreateConvert();
            var userModel = _userRepository.Get(model.UserName);

            if (userModel != null) return "the username was registered, please select a new username to register.";

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _userRepository.Create(user);
                    transaction.Commit();
                }
            }

            return message;
        }

        public void Delete(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _userRepository.Delete(id);
                    transaction.Commit();
                }
            }
        }

        public void Edit(UserLogicModel model)
        {
            var user = _userRepository.Get(model.UserId);
            user.Password = model.Password;
            user.Email = model.Email;
            user.Image = model.Image;

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _userRepository.Edit(user);
                    transaction.Commit();
                }
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

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
            if(!CheckIfExist(model.UserName))
            {
                var entity = model.ConvertToModel();
                _userRepository.Create(entity);
            }
            else
            {
                message = "Sorry, this username was registered.";
            }
            return message;
        }

        public void Edit(UserLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var entity = _userRepository.Merge(model.ConvertToModel());
                _userRepository.Edit(entity);
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
                return "The user name is not registered.";
            }
            if (user.Password.Equals(password))
            {
                return user.UserId.ToString();
            }

            return "The password is invalid.";
        }

        public IEnumerable<UserLogicModel> GetAll()
        {
            var users = _userRepository.Query().ToList();
            List<UserLogicModel> userLogicModels = new List<UserLogicModel>();
            foreach (var user in users)
            {
                var userLogicModel = user.ConvertToLogicModel();
                userLogicModels.Add(userLogicModel);
            }
            return userLogicModels;
        }

        public UserLogicModel Get(int id)
        {
            var user = _userRepository.Get(id);
            UserLogicModel userLogicModel = user.ConvertToLogicModel();
            return userLogicModel;
        }

        public List<UserLogicModel> GetByIds(int[] ids)
        {
            var users = _userRepository.Query().ToList().Where(x => ids.Contains(x.UserId));
            var userLogicModels = new List<UserLogicModel>();
            userLogicModels.AddRange(users.Select(user => user.ConvertToLogicModel()));

            return userLogicModels;
        }

        public bool CheckUserByName(string userName)
        {
            return CheckIfExist(userName);
        }

        public IEnumerable<UserLogicModel> QueryByName(string userName)
        {
            if (CheckIfExist(userName))
            {
                var users = _userRepository.Query().Where(x => x.UserName == userName).ToList();
                List<UserLogicModel> userLogicModels = new List<UserLogicModel>();
                foreach (var user in users)
                {
                    var userLogicModel = user.ConvertToLogicModel();
                    userLogicModels.Add(userLogicModel);
                }
                return userLogicModels;
            }
            else
            {
                return new List<UserLogicModel>();
            }
        }

        public string GetUserImage(string userName)
        {
            return _userRepository.Get(userName).Image;
        }

        public bool ValidateUserPassword(string useName, string password)
        {
            var user = _userRepository.Get(useName);
            return user.Password == password;
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
    }
}

using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.ILogic.LogicModel;
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

        public int Create(UserLogicModel model)
        {
            var message = 0;

            if (CheckIfExist(model.UserName)) return message;
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var user = model.ConvertToUserEntity();
                _userRepository.Create(user);
                unitOfwork.Commit();
                message = user.UserId;
            }

            return message;
        }

        public void Edit(UserLogicModel model)
        {
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var user = model.ConvertToUserEntity();
                _userRepository.Edit(user);
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

        public int Login(string userName, string password)
        {
            var user = _userRepository.Get(userName);

            if (user != null && user.Password.Equals(password))
            {
                return user.UserId;
            }

            return 0;
        }

        public IEnumerable<UserLogicModel> GetAll()
        {
            var users = _userRepository.Query();
            var userLogicModels = new List<UserLogicModel>();
            userLogicModels.AddRange(users.Select(user => user.ConvertToUserLogicModel()));

            return userLogicModels;
        }

        public UserLogicModel Get(int id)
        {
            var user = _userRepository.Get(id);
            var userLogicModel = user.ConvertToUserLogicModel();

            return userLogicModel;
        }

        public bool CheckIfExist(string userName)
        {
            return _userRepository.Query().Any(x => x.UserName == userName);
        }

        public IEnumerable<UserLogicModel> QueryByName(string userName)
        {
            var users = CheckIfExist(userName)
                ? _userRepository.Query().Where(x => x.UserName == userName).ToList()
                : new List<User>();
            var userLogicModels = new List<UserLogicModel>();
            userLogicModels.AddRange(users.Select(user => user.ConvertToUserLogicModel()));

            return userLogicModels;
        }

        public string GetUserImage(string userName)
        {
            return _userRepository.Get(userName).Image;
        }

        public IEnumerable<UserLogicModel> GetUsersByUserIds(List<int> userIds)
        {
            return userIds.Select(Get);
        }
    }
}

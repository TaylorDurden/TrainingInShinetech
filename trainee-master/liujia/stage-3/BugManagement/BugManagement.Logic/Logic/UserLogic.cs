using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.DAL.Repository;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Logic.Logic
{
    public class UserLogic: IUserLogic
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
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {                
                _userRepository.Create(model);
                unitWork.Commit();
            }            
        }

        public void Edit(User model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Edit(model);
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

        public User Get(int id)
        {
            return _userRepository.Get(id);
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

        public IEnumerable<User> GetAll()
        {
            return _userRepository.Query().AsEnumerable();
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return _userRepository.Query().Where(n => n.Email == email && n.Password == password).FirstOrDefault();
        }

        public IEnumerable<User> GetUserByWhereCondition(string whereCondition)
        {
            return _userRepository.Query().Where(n => string.IsNullOrEmpty(whereCondition) || n.FristName.Contains(whereCondition) || n.LastName.Contains(whereCondition) || n.Email.Contains(whereCondition) || n.Status.Contains(whereCondition)).AsEnumerable();
        }
    }
}

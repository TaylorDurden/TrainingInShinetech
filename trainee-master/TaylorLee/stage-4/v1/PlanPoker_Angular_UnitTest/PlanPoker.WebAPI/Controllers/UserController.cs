using System.Collections.Generic;
using System.Web.Http;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [AllowAnonymous]
        [Route("api/user")]
        [HttpPost]
        public string Create(UserViewModel user)
        {
            var userModel = user.ConvertToLogicModel();
            var result = _userLogic.Create(userModel);
            return result;
        }

        [Route("api/user")]
        [HttpDelete]
        public void Delete(int id)
        {
            _userLogic.Delete(id);
        }

        [Route("api/user")]
        [HttpPut]
        public void Edit(UserViewModel user)
        {
            var userModel = user.ConvertToLogicModel();
            _userLogic.Edit(userModel);
        }

        [Route("api/user")]
        [HttpGet]
        public IEnumerable<UserViewModel> GetAll()
        {
            var users = _userLogic.GetAll();
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                UserViewModel userViewModel = user.ConvertToViewModel();
                userViewModels.Add(userViewModel);
            }
            return userViewModels;
        }

        [AllowAnonymous]
        [Route("api/user")]
        [HttpPost]
        public string Login(string username, string password)
        {
            var result = _userLogic.Login(username, password);
            return result;
        }

        [Route("api/user/search/")]
        [HttpPost]
        public IEnumerable<UserViewModel> QueryByName(string userName)
        {
            var users = _userLogic.QueryByName(userName);
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                UserViewModel userViewModel = user.ConvertToViewModel();
                userViewModels.Add(userViewModel);
            }
            return userViewModels;
        }

        [Route("api/userCheck")]
        [HttpGet]
        public bool CheckUserByName(string userName)
        {
            return _userLogic.CheckUserByName(userName);
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public UserViewModel GetById(int id)
        {
            var user = _userLogic.Get(id);
            var userViewModel = user.ConvertToViewModel();
            return userViewModel;
        }

        [Route("api/validatePassword")]
        [HttpPost]
        public bool ValidateUserPassowrd(string userName, string password)
        {
            return _userLogic.ValidateUserPassword(userName, password);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
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
        public UserViewModel Create(UserLogicModel userLogicModel)
        {
            return userLogicModel == null
                ? null
                : _userLogic.Create(userLogicModel).ConvertToUserViewModel();
        }

        [Route("api/user/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            _userLogic.Delete(id);
        }

        [Route("api/user")]
        [HttpPut]
        public UserViewModel Edit(UserLogicModel userLogicModel)
        {
            return _userLogic.Edit(userLogicModel).ConvertToUserViewModel();
        }

        [AllowAnonymous]
        [Route("api/user/login")]
        [HttpGet]
        public UserViewModel Login(string username, string password)
        {
            return _userLogic.Login(username, password).ConvertToUserViewModel();
        }

        [Route("api/user")]
        [HttpGet]
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return _userLogic.GetAll()
                .Select(userLogicModel => userLogicModel.ConvertToUserViewModel()).ToList();
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public UserViewModel GetUserById(int id)
        {
            return _userLogic.Get(id).ConvertToUserViewModel();
        }

        [Route("api/user")]
        [HttpGet]
        public UserViewModel GetUserByName(string username)
        {
            return _userLogic.Get(username).ConvertToUserViewModel();
        }
    }
}
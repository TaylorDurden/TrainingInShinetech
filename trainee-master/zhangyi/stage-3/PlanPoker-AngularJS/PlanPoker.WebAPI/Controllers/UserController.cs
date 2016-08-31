using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;

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
        public int Create(User user)
        {
            int result = _userLogic.Create(user);
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
        public void Edit(User user)
        {
            _userLogic.Edit(user);
        }

        [Route("api/user")]
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userLogic.GetAll();
        }

        [AllowAnonymous]
        [Route("api/userLogin")]
        [HttpGet]
        public int Login(string username, string password)
        {
            int result = _userLogic.Login(username, password);
            return result;
        }

        [Route("api/user/search")]
        [HttpGet]
        public IEnumerable<User> QueryByName(string userName)
        {
            return _userLogic.QueryByName(userName);
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public User GetById(int id)
        {
            return _userLogic.Get(id);
        }
    }
}

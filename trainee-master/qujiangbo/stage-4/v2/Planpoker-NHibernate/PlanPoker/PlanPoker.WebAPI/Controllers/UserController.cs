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
        public string Create(UserWebModel userWebMoldel)
        {
            var userLogicModel = userWebMoldel.CreateConvert();
            return _userLogic.Create(userLogicModel);
        }

        [Route("api/user")]
        [HttpDelete]
        public void Delete(int id)
        {
            _userLogic.Delete(id);
        }

        [Route("api/user")]
        [HttpPut]
        public void Edit(UserWebModel userWebMoldel)
        {
            var userLogicModel = userWebMoldel.EditConvert();
            _userLogic.Edit(userLogicModel);
        }

        [Route("api/user")]
        [HttpGet]
        public List<UserWebModel> GetAll()
        {
            return _userLogic.GetAll().GetAllConvert();
        }

        [AllowAnonymous]
        [Route("api/user")]
        [HttpPost]
        public UserWebModel Login(string username, string password)
        {
            return _userLogic.Login(username, password).LoginConvert();
        }

        [Route("api/user/search/")]
        [HttpPost]
        public List<UserWebModel> QueryByName(string userName)
        {
            return _userLogic.QueryByName(userName).QueryByNameConvert();
        }

        [Route("api/usercheck")]
        [HttpGet]
        public bool CheckExist(string userName)
        {
            return _userLogic.CheckExist(userName);
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public UserWebModel GetUserById(int id)
        {
            return _userLogic.Get(id).GetUserByIdConvert();
        }
    }
}

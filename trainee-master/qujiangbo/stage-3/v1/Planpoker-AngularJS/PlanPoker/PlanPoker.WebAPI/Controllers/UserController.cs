using System;
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
        public HttpResponseMessage Create(User user)
        {
            var response= Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode=HttpStatusCode.OK;
            response.Content=new StringContent(_userLogic.Create(user));
            return response;
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
        [Route("api/user")]
        [HttpGet]
        public HttpResponseMessage Login(string username, string password)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(_userLogic.Login(username, password));
            return response;
        }

        [Route("api/user/search/")]
        [HttpPost]
        public IEnumerable<User> QueryByName(string userName)
        {
            return _userLogic.QueryByName(userName);
        }

        [Route("api/usercheck")]
        [HttpGet]
        public bool CheckExist(string userName)
        {
            return _userLogic.CheckExist(userName);
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public User GetById(int id)
        {
            return _userLogic.Get(id);
        }
    }
}

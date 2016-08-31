using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            var message = _userLogic.Create(user);

            HttpResponseMessage response;
            if (string.IsNullOrEmpty(message))
            {
                response = Request.CreateResponse(HttpStatusCode.Created, user.UserName);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(message);
            }
            return response;
            //var response= Request.CreateResponse(HttpStatusCode.OK);
            //response.StatusCode=HttpStatusCode.OK;
            //response.Content=new StringContent(_userLogic.Create(user));
            //return response;
        }

        [Route("api/user/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _userLogic.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/user")]
        [HttpPut]
        public HttpResponseMessage Edit(User user)
        {
            _userLogic.Edit(user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/user")]
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userLogic.GetAll();
        }

        [AllowAnonymous]
        [Route("api/user/login")]
        [HttpGet]
        public HttpResponseMessage Login(string username, string password)
        {
            var id = 0;
            return
                Request.CreateResponse(int.TryParse(_userLogic.Login(username, password), out id)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.Forbidden);
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public User GetById(int id)
        {
            return _userLogic.Get(id);
        }

        [Route("api/user")]
        [HttpGet]
        public User GetByName(string username)
        {
            return _userLogic.Get(username);
        }
    }
}
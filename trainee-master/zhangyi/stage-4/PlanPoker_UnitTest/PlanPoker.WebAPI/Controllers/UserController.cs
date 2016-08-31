using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Castle.Components.DictionaryAdapter;
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
        public int Create(UserViewModel userViewModel)
        {
            var userLogicModel = userViewModel.ConvertToLogicModel();

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
        public void Edit(UserViewModel userViewModel)
        {
            var userLogicModel = userViewModel.ConvertToLogicModel();
            _userLogic.Edit(userLogicModel);
        }

        [Route("api/user")]
        [HttpGet]
        public IEnumerable<UserViewModel> GetAll()
        {
            var userLogicModels = _userLogic.GetAll();
            List<UserViewModel> userViewModels = new EditableList<UserViewModel>();
            userViewModels.AddRange(userLogicModels.Select(userLogicModel => userLogicModel.ConvertToViewModel()));

            return userViewModels;
        }

        [AllowAnonymous]
        [Route("api/userLogin")]
        [HttpPost]
        public int Login(UserViewModel userViewModel)
        {
            return _userLogic.Login(userViewModel.UserName, userViewModel.Password);
        }

        [Route("api/user/search")]
        [HttpGet]
        public IEnumerable<UserViewModel> QueryByName(string userName)
        {
            var userLogicModels = _userLogic.QueryByName(userName);
            List<UserViewModel> userViewModels = new EditableList<UserViewModel>();
            userViewModels.AddRange(userLogicModels.Select(userLogicModel => userLogicModel.ConvertToViewModel()));

            return userViewModels;
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public UserViewModel GetById(int id)
        {
            var userLogicModel = _userLogic.Get(id);

            return userLogicModel.ConvertToViewModel();
        }
    }
}

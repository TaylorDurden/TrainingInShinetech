using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.ModelExchange;
using BugManagemnet.WebAPI.Models;

namespace BugManagemnet.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserLogic _userLogic;
        
        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [AllowAnonymous]
        [Route("api/user/login")]
        [HttpGet]
        public UserViewModel Login(string email, string password)
        {
            var userLogicModel = _userLogic.GetUserByEmailAndPassword(email, password);
            if (userLogicModel == null)
            {
                throw new UnauthorizedAccessException("Username or password not correctly.");
            }

            _userLogic.UpdateUserLastLoginTime(userLogicModel.UserId);
            return userLogicModel.ConvertToUserViewModel();
        }

        [AllowAnonymous]
        [Route("api/user")]
        [HttpGet]
        public JsonResult<UserListViewModel> GetUserListViewModelByCondition(string condition, string strpage,string strpagesize)
        {
            var userListViewModel = new UserListViewModel();

            var userList = _userLogic.GetUserByWhereCondition(condition);
            userListViewModel.ModelCount = userList?.Count ?? 0;
            if (userList == null || !userList.Any())
            {
                return Json(userListViewModel);
            }

            var listmodel = userList.Select(user => user.ConvertToUserViewModel()).ToList();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var pageSize = string.IsNullOrEmpty(strpagesize) ? 1 : Convert.ToInt32(strpagesize);
            var pageCount = (int)Math.Ceiling(userListViewModel.ModelCount / (double)pageSize);

            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            userListViewModel.ModelList = listmodel.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();

            return Json(userListViewModel);
        }

        [AllowAnonymous]
        [Route("api/user/register")]
        [HttpPost]
        public void CreateUserByRegister(RegisterViewModel registerViewModel)
        {
            var userLogicModel = registerViewModel.ConvertRegisterViewModelToUserLogicModel();
            _userLogic.Create(userLogicModel);
        }
        
        [Route("api/user")]
        [HttpPost]
        public void CreateUser(UserViewModel userViewModel)
        {
            var userLogicModel = userViewModel.ConvertToUserLogicModel();
            _userLogic.Create(userLogicModel);
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public UserViewModel GetUserByUserId(int id)
        {
            var userLogicModel = _userLogic.Get(id);
            return userLogicModel.ConvertToUserViewModel();
        }

        [Route("api/user/{id}")]
        [HttpDelete]
        public void DeleteUserByUserId(int id)
        {
            _userLogic.Delete(id);
        }

        [Route("api/user")]
        [HttpPut]
        public void UpdateUser(UserViewModel userViewModel)
        {
            var userLogicModel = userViewModel.ConvertToUserLogicModel();
            _userLogic.Edit(userLogicModel);
        }

        [Route("api/user")]
        [HttpGet]
        public List<UserViewModel> GetAllUser()
        {
            var model =  _userLogic.GetAll();
            return !model.Any()?null: model.ToList().Select(m => m.ConvertToUserViewModel()).ToList();
        }

        [Route("api/user/checkEmail")]
        [HttpGet]
        public bool CheckUserEmail(string email)
        {
            return _userLogic.CheckExist(email);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.WebPages.Html;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.ModelExchange;
using BugManagemnet.WebAPI.Models;
using BugManagement.Common;

namespace BugManagemnet.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserLogic _userLogic;
        private readonly int _pageSize = Constant.PageSize;
        
        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [AllowAnonymous]
        [Route("api/user/login")]
        [HttpGet]
        public UserViewModel Login(string email, string password) 
        {
            var userLogicModel = _userLogic.LoginByEmailAndPassword(email, password);
            return userLogicModel?.ConvertToUserViewModel();
        }

        [AllowAnonymous]
        [Route("api/user")]
        [HttpGet]
        public UserListViewModel GetUserListViewModelByCondition(string condition, string strpage)
        {
            var userListViewModel = new UserListViewModel();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var count = _userLogic.GetPageCountByCondition(condition);
            if (count <= 0)
            {
                return userListViewModel;
            }

            var userList = _userLogic.GetUserLogicModelsByCondition(condition, pageIndex, _pageSize,count);
            userListViewModel.Models = userList.Select(user => user.ConvertToUserViewModel()).ToList();
            userListViewModel.Pages = Constant.GetPages(_pageSize, count);

            return userListViewModel;
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
        public UserViewModel GetUserById(int id)
        {
            var userLogicModel = _userLogic.Get(id);
            return userLogicModel.ConvertToUserViewModel();
        }

        [Route("api/user/{id}")]
        [HttpDelete]
        public void DeleteUserById(int id)
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
        public List<UserViewModel> GetUsers()
        {
            var model =  _userLogic.GetAll();
            return model != null && model.Any()
                ? model.ToList().Select(m => m.ConvertToUserViewModel()).ToList()
                : null;
        }

        [Route("api/user/checkEmail")]
        [HttpGet]
        public bool CheckUserEmail(string email,string userId)
        {
            return _userLogic.CheckExist(email,userId);
        }

        [Route("api/user/getUserStatuss")]
        [HttpGet]
        public List<SelectListItem> GetUserStatuss()
        {
            var userStatuss = new List<SelectListItem>
            {
                new SelectListItem() {Text = "New", Value = "New"},
                new SelectListItem() {Text = "Checked", Value = "Checked"},
                new SelectListItem() {Text = "Delete", Value = "Delete"}
            };
            return userStatuss;
        }

        [Route("api/user/getUserTypes")]
        [HttpGet]
        public List<SelectListItem> GetUserTypes()
        {
            var userTypes = new List<SelectListItem>
            {
                new SelectListItem() {Text = "Admin", Value = "Admin"},
                new SelectListItem() {Text = "Developer", Value = "Developer"},
                new SelectListItem() {Text = "QA", Value = "QA"}
            };
            return userTypes;
        }

    }
}
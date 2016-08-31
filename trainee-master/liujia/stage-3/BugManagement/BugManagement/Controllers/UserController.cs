using BugManagement.DAL.Model;
using BugManagement.Logic;
using BugManagement.Logic.ILogic;
using BugManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugManagement.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public ActionResult Index()
        {
            InitDropDownList();

            UserListViewModel userListViewModel = this.GetUserListViewModel("", 1);

            return View("User", userListViewModel);
        }
        public ActionResult Query(UserListViewModel model, int? pageIndex)
        {
            InitDropDownList();

            model = this.GetUserListViewModel(model.whereCondition, pageIndex);

            return View("User", model);
        }
        public ActionResult Create(UserViewModel model)
        {
            User user = new User();
            common.ConvertUserViewModelToUser(model,user);
            //BugManagement.Common.ConvertModel.ConvertMoudle(model, user);
            if (model.UserId == 0)
            {
                _userLogic.Create(user);
            }
            else
            {
                _userLogic.Edit(user);
            }
            return RedirectToAction("Index");

        }
        public JsonResult GetUser(int id)
        {
            var user = new User();
            if (id != 0)
            {
                user = _userLogic.Get(id);
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(string userId)
        {
            if (userId != null && userId != "")
            {
                _userLogic.Delete(Convert.ToInt32(userId));
            }
            return RedirectToAction("Index");
        }
        private UserListViewModel GetUserListViewModel(string whereCondition, int? page)
        {
            UserListViewModel userListViewModel = new UserListViewModel();
            List<UserViewModel> listmodel = null;
            UserViewModel usermodel;
            PagingInfo<UserViewModel> userPaging = null;

            var userList = _userLogic.GetUserByWhereCondition(whereCondition);
            if (userList != null && userList.Count() > 0)
            {
                listmodel = new List<UserViewModel>();
                foreach (var user in userList)
                {
                    usermodel = new UserViewModel();
                    //BugManagement.Common.ConvertModel.ConvertMoudle(user, usermodel);
                    common.ConvertUserToUserViewModel(user,usermodel);
                    listmodel.Add(usermodel);
                }
                userPaging = new PagingInfo<UserViewModel>(PageSize, listmodel);
                userPaging.PageIndex = page ?? 1;
                userListViewModel.modelList = userPaging.GetPagingData();

            }

            userListViewModel.pagingInfo = userPaging;
            userListViewModel.model = new UserViewModel();

            return userListViewModel;
        }
        private void InitDropDownList()
        {
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem() { Text = "New", Value = "New" });
            statusList.Add(new SelectListItem() { Text = "Checked", Value = "Checked" });
            statusList.Add(new SelectListItem() { Text = "Delete", Value = "Delete" });
            statusList.Insert(0, new SelectListItem() { Text = "select status", Value = "" });
            ViewBag.StatusList = statusList;

            List<SelectListItem> typeList = new List<SelectListItem>();
            typeList.Add(new SelectListItem() { Text = "Admin", Value = "Admin" });
            typeList.Add(new SelectListItem() { Text = "Developer", Value = "Developer" });
            typeList.Add(new SelectListItem() { Text = "QA", Value = "QA" });
            typeList.Insert(0, new SelectListItem() { Text = "select type", Value = "" });
            ViewBag.TypeList = typeList;
        }
    }
}
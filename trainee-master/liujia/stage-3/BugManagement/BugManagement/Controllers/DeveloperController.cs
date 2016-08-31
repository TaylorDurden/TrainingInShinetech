using BugManagement.DAL.Model;
using BugManagement.Logic;
using BugManagement.Logic.ILogic;
using BugManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugManagement.Controllers
{
    public class DeveloperController : BaseController
    {
        private readonly IDeveloperLogic _developerLogic;
        private readonly IUserLogic _userLogic;

        public DeveloperController(IDeveloperLogic developerLogic, IUserLogic userLogic)
        {
            _developerLogic = developerLogic;
            _userLogic = userLogic;
        }
        
        public ActionResult Index()
        {
            InitDropDownList();
            DeveloperListViewModel developerListViewModel = this.GetDeveloperListViewModel("", 1);
            return View("Developer", developerListViewModel);
        }
        public ActionResult Query(DeveloperListViewModel model, int? pageIndex)
        {
            InitDropDownList();
            model = this.GetDeveloperListViewModel(model.whereCondition, pageIndex);
            return View("Developer", model);
        }
        public ActionResult Create(DeveloperViewModel model)
        {
            Developer developer = new Developer();
            //BugManagement.Common.ConvertModelToViewModel.ConvertMoudle(model, developer);
            common.ConvertDeveloperViewModelToDeveloper(model,developer);
            if (model.DeveloperId == 0)
            {
                _developerLogic.Create(developer);
            }
            else
            {
                _developerLogic.Edit(developer);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetDeveloper(int id)
        {
            var developer = new Developer();
            if (id != 0)
            {
                developer = _developerLogic.Get(id);
            }
            return Json(developer, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(string developerID)
        {
            if (developerID != null && developerID != "")
            {
                _developerLogic.Delete(Convert.ToInt32(developerID));
            }
            return RedirectToAction("Index");
        }
        private DeveloperListViewModel GetDeveloperListViewModel(string whereCondition, int? page)
        {
            DeveloperListViewModel developerListViewModel = new DeveloperListViewModel();
            List<DeveloperViewModel> listmodel = null;
            DeveloperViewModel developermodel;
            PagingInfo<DeveloperViewModel> developerPaging = null;

            var developerList = _developerLogic.GetDeveloperByWhereCondition(whereCondition);
            if (developerList != null && developerList.Count() > 0)
            {
                listmodel = new List<DeveloperViewModel>();
                foreach (var developer in developerList)
                {
                    developermodel = new DeveloperViewModel();
                    //BugManagement.Common.ConvertModelToViewModel.ConvertMoudle(developer, developermodel);
                    common.ConvertDeveloperToDeveloperViewModel(developer,developermodel);
                    listmodel.Add(developermodel);
                }
                developerPaging = new PagingInfo<DeveloperViewModel>(PageSize, listmodel);
                developerPaging.PageIndex = page ?? 1;
                developerListViewModel.modelList = developerPaging.GetPagingData();
            }

            developerListViewModel.pagingInfo = developerPaging;
            developerListViewModel.model = new DeveloperViewModel();

            return developerListViewModel;
        }
        private void InitDropDownList()
        {
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem() { Text = "status1", Value = "status1" });
            statusList.Add(new SelectListItem() { Text = "status2", Value = "status2" });
            statusList.Add(new SelectListItem() { Text = "status3", Value = "status3" });
            statusList.Add(new SelectListItem() { Text = "status4", Value = "status4" });
            statusList.Insert(0, new SelectListItem() { Text = "select status", Value = "" });
            ViewBag.StatusList = statusList;
            
            List<SelectListItem> userList = new List<SelectListItem>();
            List<User> userdbList = _userLogic.GetAll().ToList();
            if (userdbList != null && userdbList.Count() > 0)
            {
                foreach (var user in userdbList)
                {
                    userList.Add(new SelectListItem() { Text = user.FristName + " " + user.LastName,Value = user.UserId.ToString() });
                }                
            }
            userList.Insert(0, new SelectListItem() { Text = "select user", Value = "" });
            ViewBag.UserList = userList;
        }
    }
}
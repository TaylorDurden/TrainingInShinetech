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
    public class DashboardController : BaseController
    {
        private readonly IBugLogic _bugLogic;
        private readonly IBugTypeLogic _bugTypeLogic;
        private readonly IProjectLogic _projectLogic;
        private readonly IDeveloperLogic _developerLogic;

        public DashboardController(IBugLogic bugLogic, IBugTypeLogic bugTypeLogic, IProjectLogic projectLogic, IDeveloperLogic developerLogic)
        {
            _bugLogic = bugLogic;
            _bugTypeLogic = bugTypeLogic;
            _projectLogic = projectLogic;
            _developerLogic = developerLogic;
        }
        
        public ActionResult Index()
        {
            InitDropDownList();

            DashboardViewModel dashboardViewModel = this.GetDashboardViewModelByCondition(string.Empty);

            return View("Dashboard", dashboardViewModel);
        }
        public ActionResult Query(DashboardViewModel model)
        {
            InitDropDownList();

            model = this.GetDashboardViewModelByCondition(model.whereCondition);

            return View("Dashboard", model);
        }
        private DashboardViewModel GetDashboardViewModelByCondition(string whereCondition)
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.assignedBugList = this.GetBugViewModelByStatus(whereCondition,"Assigned");
            dashboardViewModel.inProgressBugList = this.GetBugViewModelByStatus(whereCondition,"InProgress");
            dashboardViewModel.inTestBugList = this.GetBugViewModelByStatus(whereCondition,"InTest");
            dashboardViewModel.doneBugList = this.GetBugViewModelByStatus(whereCondition,"Done");
            return dashboardViewModel;
        }
        private List<BugViewModel> GetBugViewModelByStatus(string whereCondition,string strStatus)
        {
            List<BugViewModel> bugViewModelList = null;
            BugViewModel bugViewModel;
            List<Bug> bugList = _bugLogic.GetBugBywhereConditionAndStatus(whereCondition, strStatus).ToList();
            if (bugList != null && bugList.Count() > 0)
            {
                bugViewModelList = new List<BugViewModel>();
                foreach (var bug in bugList)
                {
                    bugViewModel = new BugViewModel();
                    //BugManagement.Common.ConvertModel.ConvertMoudle(bug, bugViewModel);
                    common.ConvertBugToBugViewModel(bug,bugViewModel);
                    bugViewModelList.Add(bugViewModel);
                }
            }
            return bugViewModelList;
        }
        private void InitDropDownList()
        {
            List<SelectListItem> typeList = new List<SelectListItem>();
            var typedbList = _bugTypeLogic.GetAll().ToList();
            if (typedbList != null && typedbList.Count() > 0)
            {
                foreach (var type in typedbList)
                {
                    typeList.Add(new SelectListItem() { Text = type.Name, Value = type.BugTypeId.ToString() });
                }
            }
            typeList.Insert(0, new SelectListItem() { Text = "select type", Value = "" });
            ViewBag.TypeList = typeList;

            List<SelectListItem> projectList = new List<SelectListItem>();
            List<Project> projectdbList = _projectLogic.GetAll().ToList();
            if (projectdbList != null && projectdbList.Count() > 0)
            {
                foreach (var project in projectdbList)
                {
                    projectList.Add(new SelectListItem() { Text = project.ProjectName, Value = project.ProjectId.ToString() });
                }
            }
            projectList.Insert(0, new SelectListItem() { Text = "select projects", Value = "" });
            ViewBag.ProjectList = projectList;

            List<SelectListItem> developerList = new List<SelectListItem>();
            List<Developer> developerdbList = _developerLogic.GetAll().ToList();
            if (developerdbList != null && developerdbList.Count() > 0)
            {
                foreach (var develoer in developerdbList)
                {
                    developerList.Add(new SelectListItem() { Text = develoer.FristName + " " + develoer.LastName, Value = develoer.DeveloperId.ToString() });
                }
            }
            ViewBag.DeveloperList = developerList;

            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem() { Text = "New", Value = "New" });
            statusList.Add(new SelectListItem() { Text = "Assigned", Value = "Assigned" });
            statusList.Add(new SelectListItem() { Text = "InProgress", Value = "InProgress" });
            statusList.Add(new SelectListItem() { Text = "InTest", Value = "InTest" });
            statusList.Add(new SelectListItem() { Text = "Done", Value = "Done" });
            ViewBag.StatusList = statusList;
        }
    }
}
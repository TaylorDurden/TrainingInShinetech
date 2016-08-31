using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using BugManagement.Common;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.Models;
using BugManagemnet.WebAPI.ModelExchange;

namespace BugManagemnet.WebAPI.Controllers
{
    public class BugController : ApiController
    {
        private readonly IBugLogic _bugLogic;
        private readonly int _pageSize = Constant.PageSize;

        public BugController(IBugLogic bugLogic)
        {
            _bugLogic = bugLogic;
        }

        [System.Web.Http.Route("api/bug")]
        [System.Web.Http.HttpPost]
        public void CreateBug(BugCommand command)
        {
            var bugLogicModel = command.BugViewModel.ConvertToBugLogicModel();
            var developerLogicModels = command.DeveloperViewModels.ConvertToDeveloperViewModels();
            _bugLogic.Create(bugLogicModel, developerLogicModels, bugLogicModel.StrDocuments);
        }

        [System.Web.Http.Route("api/bug")]
        [System.Web.Http.HttpPut]
        public void UpdateBug(BugCommand command)
        {
            var bugLogicModel = command.BugViewModel.ConvertToBugLogicModel();
            var developerLogicModels = command.DeveloperViewModels.ConvertToDeveloperViewModels();
            _bugLogic.Edit(bugLogicModel, developerLogicModels, bugLogicModel.StrDocuments);
        }

        [System.Web.Http.Route("api/bug")]
        [System.Web.Http.HttpGet]
        public BugListViewModel GetBugListViewModelByCondition(string condition, string strpage)
        {
            var bugListViewModel = new BugListViewModel();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var count = _bugLogic.GetPageCountByCondition(condition);
            if (count <= 0)
            {
                return bugListViewModel;
            }

            var bugList = _bugLogic.GetBugLogicModelsByCondition(condition, pageIndex, _pageSize, count);
            bugListViewModel.Models = bugList.Select(bug => bug.ConvertToBugViewModel()).ToList();
            bugListViewModel.Pages = Constant.GetPages(_pageSize, count);

            return bugListViewModel;
        }

        [System.Web.Http.Route("api/getDashboard")]
        [System.Web.Http.HttpGet]
        public DashboardViewModel GetDashboardViewModelByCondition(string condition)
        {
            var dashboardViewModel = new DashboardViewModel();
            var bugLogicModels = _bugLogic.GetBugLogicModelsBySerchCondition(condition);
            if (bugLogicModels == null || !bugLogicModels.Any())
            {
                return dashboardViewModel;
            }

            dashboardViewModel.AssignedBugList = bugLogicModels.Where(n => n.Status == "Assigned").ToList().ConvertToBugViewModels();
            dashboardViewModel.InProgressBugList = bugLogicModels.Where(n => n.Status == "InProgress").ToList().ConvertToBugViewModels();
            dashboardViewModel.InTestBugList = bugLogicModels.Where(n => n.Status == "InTest").ToList().ConvertToBugViewModels();
            dashboardViewModel.DoneBugList = bugLogicModels.Where(n => n.Status == "Done").ToList().ConvertToBugViewModels();


            return dashboardViewModel;
        }

        [System.Web.Http.Route("api/UpdateBugStatus")]
        [System.Web.Http.HttpGet]
        public bool UpdateBugStatus(string bugId, string stauts)
        {
            var result = true;
            if (!string.IsNullOrEmpty(bugId) && !string.IsNullOrEmpty(stauts))
            {
               _bugLogic.UpdateBugStatus(Convert.ToInt32(bugId), stauts);
            }
            else
            {
                result = false;
            }

            return result;
        }

        [System.Web.Http.Route("api/bug/{id}")]
        [System.Web.Http.HttpGet]
        public BugViewModel GetBugById(int id)
        {
            var bugLogicModel = _bugLogic.Get(id);
            return bugLogicModel.ConvertToBugViewModel();
        }

        [System.Web.Http.Route("api/bug/{id}")]
        [System.Web.Http.HttpDelete]
        public void DeleteBugById(int id)
        {
            _bugLogic.Delete(id);
        }

        [System.Web.Http.Route("api/bug/getBugStatuss")]
        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetBugStatuss()
        {
            var bugStatuss = new List<SelectListItem>
            {
                new SelectListItem() {Text = "New", Value = "New"},
                new SelectListItem() {Text = "Assigned", Value = "Assigned"},
                new SelectListItem() {Text = "InProgress", Value = "InProgress"},
                new SelectListItem() {Text = "InTest", Value = "InTest"},
                new SelectListItem() {Text = "Done", Value = "Done"}
            };
            return bugStatuss;
        }
    }
}
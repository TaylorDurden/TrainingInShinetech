using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.Models;
using BugManagemnet.WebAPI.ModelExchange;
using System.Web.Mvc;
using BugManagement.Common;

namespace BugManagemnet.WebAPI.Controllers
{
    public class DeveloperController : ApiController
    {
        private readonly IDeveloperLogic _developerLogic;
        private readonly ICauseBugDeveloperLogic _causeBugDeveloperLogic;
        private readonly int _pageSize = Constant.PageSize;

        public DeveloperController(IDeveloperLogic developerLogic, ICauseBugDeveloperLogic causeBugDeveloperLogic)
        {
            _developerLogic = developerLogic;
            _causeBugDeveloperLogic = causeBugDeveloperLogic;
        }

        [System.Web.Http.Route("api/developer")]
        [System.Web.Http.HttpGet]
        public DeveloperListViewModel GetDeveloperListViewModelByCondition(string condition, string strpage)
        {
            var listViewModels = new DeveloperListViewModel();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var count = _developerLogic.GetPageCountByCondition(condition);
            if (count <= 0)
            {
                return listViewModels;
            }

            var developerList = _developerLogic.GetDeveloperLogicModelsByCondition(condition, pageIndex, _pageSize, count);
            listViewModels.Models = developerList.Select(developer => developer.ConvertToDeveloperViewModel()).ToList();
            listViewModels.Pages = Constant.GetPages(_pageSize, count);

            return listViewModels;
        }

        [System.Web.Http.Route("api/allDeveloper")]
        [System.Web.Http.HttpGet]
        public List<DeveloperViewModel> GetDeveloperViewModel()
        {
            var developerList = _developerLogic.GetAll();
            return developerList.ConvertToDeveloperViewModels();
        }

        [System.Web.Http.Route("api/developer")]
        [System.Web.Http.HttpGet]
        public List<DeveloperViewModel> GetDeveloperViewModelByBugId(string bugId)
        {
            List<DeveloperViewModel> listmodel = null;
            var causeDevelopers = _causeBugDeveloperLogic.GetCauseBugDeveloperByBugId(Convert.ToInt32(bugId));
            if (causeDevelopers != null && causeDevelopers.Any())
            {
                listmodel = causeDevelopers.Select(causeDeveloper => _developerLogic.Get(causeDeveloper.DeveloperId)).ToList().ConvertToDeveloperViewModels();
            }

            return listmodel;
        }

        [System.Web.Http.Route("api/developer/{id}")]
        [System.Web.Http.HttpGet]
        public DeveloperViewModel GetDeveloperById(int id)
        {
            var developerLogicModel = _developerLogic.Get(id);
            return developerLogicModel.ConvertToDeveloperViewModel();
        }

        [System.Web.Http.Route("api/developer/{id}")]
        [System.Web.Http.HttpDelete]
        public void DeleteDeveloperById(int id)
        {
            _developerLogic.Delete(id);
        }

        [System.Web.Http.Route("api/developer")]
        [System.Web.Http.HttpPut]
        public void UpdateDeveloper(DeveloperViewModel developerViewModel)
        {
            var developerLogicModel = developerViewModel.ConvertToDeveloperLogicModel();
            _developerLogic.Edit(developerLogicModel);
        }

        [System.Web.Http.Route("api/developer")]
        [System.Web.Http.HttpPost]
        public void CreateDeveloper(DeveloperViewModel developerViewModel)
        {
            var developerLogicModel = developerViewModel.ConvertToDeveloperLogicModel();
            _developerLogic.Create(developerLogicModel);
        }

        [System.Web.Http.Route("api/developer/getDeveloperStatuss")]
        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetDeveloperStatuss()
        {
            var developerStatuss = new List<SelectListItem>
            {
                new SelectListItem() {Text = "status1", Value = "status1"},
                new SelectListItem() {Text = "status2", Value = "status2"},
                new SelectListItem() {Text = "status3", Value = "status3"},
                new SelectListItem() {Text = "status4", Value = "status4"}
            };
            return developerStatuss;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.Models;
using BugManagemnet.WebAPI.ModelExchange;

namespace BugManagemnet.WebAPI.Controllers
{
    public class DeveloperController: ApiController
    {
        private readonly IDeveloperLogic _developerLogic;
        private readonly ICauseBugDeveloperLogic _causeBugDeveloperLogic;

        public DeveloperController(IDeveloperLogic developerLogic, ICauseBugDeveloperLogic causeBugDeveloperLogic)
        {
            _developerLogic = developerLogic;
            _causeBugDeveloperLogic = causeBugDeveloperLogic;
        }

        [Route("api/developer")]
        [HttpGet]
        public JsonResult<DeveloperListViewModel> GetDeveloperListViewModelByCondition(string condition, string strpage,string strpagesize)
        {
            var listViewModels = new DeveloperListViewModel();
            
            var developerList = _developerLogic.GetDeveloperByWhereCondition(condition);
            listViewModels.ModelCount = developerList?.Count ?? 0;
            if (developerList == null || !developerList.Any())
            {
                return Json(listViewModels);
            }

            var listmodel = developerList.Select(developer => developer.ConvertToDeveloperViewModel()).ToList();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var pageSize = string.IsNullOrEmpty(strpagesize) ? 1 : Convert.ToInt32(strpagesize);
            var pageCount = (int)Math.Ceiling(listViewModels.ModelCount / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
                
            listViewModels.ModelList = listmodel.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();

            return Json(listViewModels);
        }

        [Route("api/allDeveloper")]
        [HttpGet]
        public JsonResult<List<DeveloperViewModel>> GetDeveloperViewModel()
        {
            var developerList = _developerLogic.GetAll();
            return Json(developerList.ConvertToDeveloperViewModels());
        }

        [Route("api/developer")]
        [HttpGet]
        public JsonResult<List<DeveloperViewModel>> GetDeveloperViewModelByBugId(string bugId)
        {
            List<DeveloperViewModel> listmodel = null;
            var causeDevelopers = _causeBugDeveloperLogic.GetCauseBugDeveloperByBugId(Convert.ToInt32(bugId));
            if (causeDevelopers != null && causeDevelopers.Any())
            {
                listmodel = causeDevelopers.Select(causeDeveloper => _developerLogic.Get(causeDeveloper.DeveloperId)).ToList().ConvertToDeveloperViewModels();
            }

            return Json(listmodel);
        }

        [Route("api/developer/{id}")]
        [HttpGet]
        public DeveloperViewModel GetDeveloperByDeveloperId(int id)
        {
            var developerLogicModel = _developerLogic.Get(id);
            return developerLogicModel.ConvertToDeveloperViewModel();
        }

        [Route("api/developer/{id}")]
        [HttpDelete]
        public void DeleteDeveloperByDeveloperId(int id)
        {
            _developerLogic.Delete(id);
        }

        [Route("api/developer")]
        [HttpPut]
        public void UpdateDeveloper(DeveloperViewModel developerViewModel)
        {
            var developerLogicModel = developerViewModel.ConvertToDeveloperLogicModel();
            _developerLogic.Edit(developerLogicModel);
        }

        [Route("api/developer")]
        [HttpPost]
        public void CreateDeveloper(DeveloperViewModel developerViewModel)
        {
            var developerLogicModel = developerViewModel.ConvertToDeveloperLogicModel();
            _developerLogic.Create(developerLogicModel);
        }
    }
}
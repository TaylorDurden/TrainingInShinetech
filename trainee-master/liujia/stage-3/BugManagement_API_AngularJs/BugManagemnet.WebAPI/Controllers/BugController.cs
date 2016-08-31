using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.Models;
using BugManagemnet.WebAPI.ModelExchange;

namespace BugManagemnet.WebAPI.Controllers
{
    public class BugController : ApiController
    {
        private readonly IBugLogic _bugLogic;

        public BugController(IBugLogic bugLogic)
        {
            _bugLogic = bugLogic;
        }

        [Route("api/bug")]
        [HttpPost]
        public void Create(BugViewModel model)
        {
            var bugLogicModel = model.ConvertToBugLogicModel();
            _bugLogic.Create(bugLogicModel, model.StrDevelopers, model.StrDocuments);
        }

        [Route("api/bug")]
        [HttpPut]
        public void Edit(BugViewModel model)
        {
            var bugLogicModel = model.ConvertToBugLogicModel();
            _bugLogic.Edit(bugLogicModel, model.StrDevelopers, model.StrDocuments);
        }

        [Route("api/bug")]
        [HttpGet]
        public JsonResult<BugListViewModel> GetBugListViewModelByCondition(string condition, string strpage, string strpagesize)
        {
            var bugListViewModel = new BugListViewModel();

            var bugList = _bugLogic.GetBugByWhereCondition(condition);
            bugListViewModel.ModelCount = bugList?.Count ?? 0;

            if (bugList == null || !bugList.Any())
            {
                return Json(bugListViewModel);
            }

            var listmodel = bugList.Select(bug => bug.ConvertToBugViewModel()).ToList();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var pageSize = string.IsNullOrEmpty(strpagesize) ? 1 : Convert.ToInt32(strpagesize);
            var pageCount = (int)Math.Ceiling(bugListViewModel.ModelCount / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            bugListViewModel.ModelList = listmodel.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return Json(bugListViewModel);
        }

        [Route("api/getDashboard")]
        [HttpGet]
        public JsonResult<DashboardViewModel> GetDashboardViewModelByCondition(string condition)
        {
            var dashboardViewModel = new DashboardViewModel();
            var bugLogicModels = _bugLogic.GetBugByWhereCondition(condition);
            if (bugLogicModels == null || !bugLogicModels.Any())
            {
                return Json(dashboardViewModel);
            }
            
            dashboardViewModel.AssignedBugList = bugLogicModels.Where(n => n.Status == "Assigned").ToList().ConvertToBugViewModels();
            dashboardViewModel.InProgressBugList = bugLogicModels.Where(n => n.Status == "InProgress").ToList().ConvertToBugViewModels();
            dashboardViewModel.InTestBugList = bugLogicModels.Where(n => n.Status == "InTest").ToList().ConvertToBugViewModels();
            dashboardViewModel.DoneBugList = bugLogicModels.Where(n => n.Status == "Done").ToList().ConvertToBugViewModels();

            return Json(dashboardViewModel);
        }


        [Route("api/UpdateBugStatus")]
        [HttpGet]
        public bool UpdateBugStatus(string bugId, string stauts)
        {
            var result = true;
            if (!string.IsNullOrEmpty(bugId) && !string.IsNullOrEmpty(stauts))
            {
                try
                {
                    _bugLogic.UpdateBugStatus(Convert.ToInt32(bugId), stauts);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        [Route("api/bug/{id}")]
        [HttpGet]
        public BugViewModel GetBugByBugId(int id)
        {
            var bugLogicModel = _bugLogic.Get(id);
            return bugLogicModel.ConvertToBugViewModel();
        }

        [Route("api/bug/{id}")]
        [HttpDelete]
        public void DeleteBugByBugId(int id)
        {
            _bugLogic.Delete(id);
        }


    }
}
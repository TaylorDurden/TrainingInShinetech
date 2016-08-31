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
    public class ProjectController : ApiController
    {
        private readonly IProjectLogic _projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            _projectLogic = projectLogic;
        }

        [Route("api/project")]
        [HttpGet]
        public JsonResult<ProjectListViewModel> GetProjectListViewModelByCondition(string condition, string strpage, string strpagesize)
        {
            var projectListViewModel = new ProjectListViewModel();

            var projectList = _projectLogic.GetProjectByWhereCondition(condition);
            projectListViewModel.ModelCount = projectList ?.Count ?? 0;
            if (projectList == null || !projectList.Any())
            {
                return Json(projectListViewModel);
            }

            var listmodel = projectList.Select(project => project.ConvertToProjectViewModel()).ToList();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var pageSize = string.IsNullOrEmpty(strpagesize) ? 1 : Convert.ToInt32(strpagesize);
            var pageCount = (int)Math.Ceiling(projectListViewModel.ModelCount / (double)pageSize);

            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            projectListViewModel.ModelList = listmodel.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();

            return Json(projectListViewModel);
        }

        [Route("api/allProject")]
        [HttpGet]
        public JsonResult<List<ProjectViewModel>> GetAllProjectListViewModel()
        {
            List<ProjectViewModel> listmodel = null;
            var projectList = _projectLogic.GetAll();
            if (projectList != null && projectList.Any())
            {
                listmodel = projectList.Select(project => project.ConvertToProjectViewModel()).ToList();
            }
            return Json(listmodel);
        }

        [Route("api/project/{id}")]
        [HttpGet]
        public ProjectViewModel GetProjectByProjectId(int id)
        {
            var projectLogicModel = _projectLogic.Get(id);
            return projectLogicModel.ConvertToProjectViewModel();
        }

        [Route("api/project/{id}")]
        [HttpDelete]
        public void DeleteProjectByProjectId(int id)
        {
            _projectLogic.Delete(id);
        }

        [Route("api/project")]
        [HttpPut]
        public void UpdateProject(ProjectViewModel projectViewModel)
        {
            var projectLogicModel = projectViewModel.ConvertToProjectLogicModel();
            _projectLogic.Edit(projectLogicModel);
        }

        [Route("api/project")]
        [HttpPost]
        public void CreateProject(ProjectViewModel projectViewModel)
        {
            var projectLogicModel = projectViewModel.ConvertToProjectLogicModel();
            _projectLogic.Create(projectLogicModel);
        }

        [Route("api/project/checkProjectName")]
        [HttpGet]
        public bool CheckProjectNameIsExist(string projectName)
        {
            return _projectLogic.CheckExist(projectName);
        }
    }
}
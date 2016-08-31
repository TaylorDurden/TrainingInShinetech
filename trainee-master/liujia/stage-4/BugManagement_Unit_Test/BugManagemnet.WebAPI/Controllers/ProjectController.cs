using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BugManagement.Common;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.Models;
using BugManagemnet.WebAPI.ModelExchange;

namespace BugManagemnet.WebAPI.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProjectLogic _projectLogic;
        private readonly int _pageSize = Constant.PageSize;

        public ProjectController(IProjectLogic projectLogic)
        {
            _projectLogic = projectLogic;
        }

        [Route("api/project")]
        [HttpGet]
        public ProjectListViewModel GetProjectListViewModelByCondition(string condition, string strpage)
        {
            var projectListViewModel = new ProjectListViewModel();
            var pageIndex = string.IsNullOrEmpty(strpage) ? 1 : Convert.ToInt32(strpage);
            var count = _projectLogic.GetPageCountByCondition(condition);
            if (count <= 0)
            {
                return projectListViewModel;
            }

            var projectList = _projectLogic.GetProjectLogicModelsByCondition(condition, pageIndex, _pageSize, count);
            projectListViewModel.Models = projectList.Select(project => project.ConvertToProjectViewModel()).ToList();
            projectListViewModel.Pages = Constant.GetPages(_pageSize, count);

            return projectListViewModel;
        }

        [Route("api/allProject")]
        [HttpGet]
        public List<ProjectViewModel> GetProjects()
        {
            List<ProjectViewModel> listmodel = null;
            var projectList = _projectLogic.GetAll();
            if (projectList != null && projectList.Any())
            {
                listmodel = projectList.Select(project => project.ConvertToProjectViewModel()).ToList();
            }
            return listmodel;
        }

        [Route("api/project/{id}")]
        [HttpGet]
        public ProjectViewModel GetProjectById(int id)
        {
            var projectLogicModel = _projectLogic.Get(id);
            return projectLogicModel.ConvertToProjectViewModel();
        }

        [Route("api/project/{id}")]
        [HttpDelete]
        public void DeleteProjectById(int id)
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
        public bool CheckProjectNameIsExist(string projectName,string projectId)
        {
            return _projectLogic.CheckExist(projectName, projectId);
        }
    }
}
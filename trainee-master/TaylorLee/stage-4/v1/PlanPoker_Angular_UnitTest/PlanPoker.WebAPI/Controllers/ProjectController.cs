using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebAPI.Controllers
{

    public class ProjectController : ApiController
    {
        private readonly IProjectLogic _projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            _projectLogic = projectLogic;
        }

        [Route("api/project")]
        [HttpPost]
        public ProjectViewModel Create(ProjectViewModel projectViewModel)
        {
            projectViewModel.ProjectGuid = Guid.NewGuid().ToString();
            var projectLogicModel = _projectLogic.Create(projectViewModel.ConvertToLogicModel());
            return projectLogicModel.ConvertToViewModel();
        }

        [Route("api/project")]
        [HttpDelete]
        public void Delete(int id)
        {
            _projectLogic.Delete(id);
        }

        [Route("api/project")]
        [HttpPut]
        public void Edit(ProjectViewModel projectViewModel)
        {
            _projectLogic.Edit(projectViewModel.ConvertToLogicModel());
        }

        [Route("api/project")]
        [HttpGet]
        public IEnumerable<ProjectViewModel> GetAll()
        {
            var projectLogicModels = _projectLogic.GetAll();
            return projectLogicModels.Select(projectLogicModel => projectLogicModel.ConvertToViewModel()).ToList();
        }

        [Route("api/project/{id}")]
        [HttpGet]
        public ProjectViewModel GetProjectById(int id)
        {
            var projectLogicModel = _projectLogic.Get(id);
            ProjectViewModel projectViewModel = projectLogicModel.ConvertToViewModel();
            return projectViewModel;
        }

        [Route("api/getprojecturl/{id}")]
        [HttpGet]
        public string GetProjectUrlById(int id)
        {
            string url = ConfigurationManager.AppSettings["WebApplicationPath"] + "/views/ProjectEstimate.html?presenter=1&projectGuid=";
            return url + _projectLogic.Get(id).ProjectGuid;
        }

        [Route("api/project")]
        [HttpGet]
        public IEnumerable<ProjectViewModel> GetProjectByName(string name)
        {
            var projectLogicModels = _projectLogic.Get(name);
            return projectLogicModels.Select(projectLogicModel => projectLogicModel.ConvertToViewModel()).ToList();
        }

        [Route("api/projectcheck")]
        [HttpGet]
        public bool CheckExist(string projectName)
        {
            return _projectLogic.CheckExist(projectName);
        }
    }
}

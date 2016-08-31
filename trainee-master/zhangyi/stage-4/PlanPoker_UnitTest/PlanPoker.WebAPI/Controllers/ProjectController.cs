using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Castle.Components.DictionaryAdapter;
using PlanPoker.ILogic;
using PlanPoker.ILogic.LogicModel;
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
        public void Create(ProjectViewModel projectViewModel)
        {
            if (projectViewModel == null) return;

            var projectLogicModel = projectViewModel.ConvertToProjectLogicModel();
            _projectLogic.Create(projectLogicModel);
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
            ProjectLogicModel projectLogicModel = projectViewModel.ConvertToProjectLogicModel();
            _projectLogic.Edit(projectLogicModel);
        }

        [Route("api/project")]
        [HttpGet]
        public IEnumerable<ProjectViewModel> GetAll()
        {
            var projectLogicModels = _projectLogic.GetAll();
            var projectViewModels = new List<ProjectViewModel>();
            projectViewModels.AddRange(projectLogicModels.Select(projectLogicModel => projectLogicModel.ConvertToProjectViewModel()));

            return projectViewModels;
        }

        [Route("api/project/{id}")]
        [HttpGet]
        public ProjectViewModel GetProjectById(int id)
        {
            var projectLogicModel = _projectLogic.Get(id);

            return projectLogicModel.ConvertToProjectViewModel();
        }

        [Route("api/getprojecturl/{id}")]
        [HttpGet]
        public string GetProjectUrlById(int id)
        {
            return _projectLogic.Get(id) != null ? _projectLogic.Get(id).ProjectGuid.ToString() : "";
        }

        [Route("api/project")]
        [HttpGet]
        public IEnumerable<ProjectViewModel> GetProjectByName(string name)
        {
            var projectLogicModels = _projectLogic.Get(name);
            List<ProjectViewModel> projectViewModels = new EditableList<ProjectViewModel>();
            projectViewModels.AddRange(projectLogicModels.Select(projectLogicModel => projectLogicModel.ConvertToProjectViewModel()));

            return projectViewModels;
        }

        [Route("api/projectcheck")]
        [HttpGet]
        public bool CheckExist(string projectName)
        {
            return _projectLogic.CheckExist(projectName);
        }

        [Route("api/projectid/{id}")]
        [HttpGet]
        public ProjectViewModel GetProjectByGuid(Guid id)
        {
            var projectLogicModel = _projectLogic.GetAll().FirstOrDefault(x => x.ProjectGuid == id);

            return projectLogicModel.ConvertToProjectViewModel();
        }
    }
}

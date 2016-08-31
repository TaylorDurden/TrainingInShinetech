using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
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
        public ProjectViewModel Create(ProjectLogicModel projectLogicModel)
        {
            return projectLogicModel == null
                ? null
                : _projectLogic.Create(projectLogicModel).ConvertToProjectViewModel();
        }

        [Route("api/project/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            _projectLogic.Delete(id);
        }

        [Route("api/project")]
        [HttpPut]
        public ProjectViewModel Edit(ProjectLogicModel projectLogicModel)
        {
            return _projectLogic.Edit(projectLogicModel).ConvertToProjectViewModel();
        }

        [Route("api/project")]
        [HttpGet]
        public IEnumerable<ProjectViewModel> GetAllProjects()
        {
            return _projectLogic.GetAll()
                .Select(projectLogicModel => projectLogicModel.ConvertToProjectViewModel());
        }

        [Route("api/projectid")]
        [HttpGet]
        public ProjectViewModel GetProjectById(int id)
        {
            return _projectLogic.Get(id).ConvertToProjectViewModel();
        }

        [Route("api/project")]
        [HttpGet]
        public ProjectViewModel GetProjectByName(string name)
        {
            return _projectLogic.Get(name).ConvertToProjectViewModel();
        }
    }
}
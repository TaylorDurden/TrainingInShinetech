using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;

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
        public void Create(Project project)
        {
            if (project != null)
            {
                project.ProjectGuid = Guid.NewGuid();
                _projectLogic.Create(project);
            }
        }

        [Route("api/project")]
        [HttpDelete]
        public void Delete(int id)
        {
            _projectLogic.Delete(id);
        }

        [Route("api/project")]
        [HttpPut]
        public void Edit(Project project)
        {
            _projectLogic.Edit(project);
        }

        [Route("api/project")]
        [HttpGet]
        public IEnumerable<Project> GetAll()
        {
            return _projectLogic.GetAll();
        }

        [Route("api/project/{id}")]
        [HttpGet]
        public Project GetProjectById(int id)
        {
            return _projectLogic.Get(id);
        }

        [Route("api/getprojecturl/{id}")]
        [HttpGet]
        public string GetProjectUrlById(int id)
        {
            if (_projectLogic.Get(id) != null)
            {
                return _projectLogic.Get(id).ProjectGuid.ToString();
            }
            return "";
        }

        [Route("api/project")]
        [HttpGet]
        public IEnumerable<Project> GetProjectByName(string name)
        {
            return _projectLogic.Get(name);
        }

        [Route("api/projectcheck")]
        [HttpGet]
        public bool CheckExist(string projectName)
        {
            return _projectLogic.CheckExist(projectName);
        }

        [Route("api/projectid/{id}")]
        [HttpGet]
        public Project GetProjectByGuid(Guid id)
        {
            return _projectLogic.GetAll().Where(x => x.ProjectGuid == id).FirstOrDefault();
        }
    }
}

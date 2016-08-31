using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage Create(Project project)
        {
            project.ProjectGuid = Guid.NewGuid();
            var message = _projectLogic.Create(project);

            HttpResponseMessage response;
            if (string.IsNullOrEmpty(message))
            {
                response = Request.CreateResponse(HttpStatusCode.Created, project);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(message);
            }
            return response;
        }

        [Route("api/project/{id}")]
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
            return _projectLogic.GetGuidUrl(id);
        }

        [Route("api/project")]
        [HttpGet]
        public HttpResponseMessage GetProjectByName(string name)
        {
            var project = _projectLogic.Get(name);

            return Request.CreateResponse(HttpStatusCode.OK, project);
        }

        [Route("api/projectcheck")]
        [HttpGet]
        public bool CheckExist(string projectName)
        {
            return _projectLogic.CheckIfExist(projectName);
        }

        [Route("api/projectid/{id}")]
        [HttpGet]
        public string GetProjectGuidById(Guid id)
        {
            return _projectLogic.GetAll().Where(x => x.ProjectGuid == id).First().Id.ToString();
        }
    }
}
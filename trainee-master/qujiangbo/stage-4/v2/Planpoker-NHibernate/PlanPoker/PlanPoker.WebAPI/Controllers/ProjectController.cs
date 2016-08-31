using System;
using System.Collections.Generic;
using System.Configuration;
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
        public void Create(ProjectWebModel projectWebModel)
        {
            projectWebModel.ProjectGuid = Guid.NewGuid();
            var projectLogicModel = projectWebModel.CreateConvert();
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
        public void Edit(ProjectWebModel projectWebModel)
        {
            var projectLogicModel = projectWebModel.EditConvert();
            _projectLogic.Edit(projectLogicModel);
        }

        [Route("api/project")]
        [HttpGet]
        public List<ProjectWebModel> GetAll()
        {
            return _projectLogic.GetAll().GetAllConvert();
        }

        [Route("api/project/{id}")]
        [HttpGet]
        public ProjectWebModel GetProjectById(int id)
        {
            return _projectLogic.Get(id).GetProjectByIdConvert();
        }

        [Route("api/getprojecturl/{id}")]
        [HttpGet]
        public string GetProjectUrlById(int id)
        {
            var url = ConfigurationManager.AppSettings["WebApplicationPath"] + "/#/projectEstimate/presenter/";
            return url + _projectLogic.Get(id).GetProjectUrlByIdConvert();
        }

        [Route("api/project")]
        [HttpGet]
        public List<ProjectWebModel> GetProjectByName(string name)
        {
            return _projectLogic.Get(name).GetProjectByNameConvert();
        }

        [Route("api/projectcheck")]
        [HttpGet]
        public bool CheckExist(string projectName)
        {
            return _projectLogic.CheckExist(projectName);
        }

        [Route("api/projectid/{id}")]
        [HttpGet]
        public string GetProjectGuidById(Guid id)
        {
            return _projectLogic.GetAll().First(x => x.ProjectGuid == id).Id.ToString();
        }

        [Route("api/projectguid/{id}")]
        [HttpGet]
        public ProjectWebModel GetProjectByGuid(Guid id)
        {
            return _projectLogic.GetAll().First(x => x.ProjectGuid == id).GetProjectByGuidConvert();
        }
    }
}

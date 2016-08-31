using BugManagement.DAL.Model;
using BugManagement.Logic;
using BugManagement.Logic.ILogic;
using BugManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugManagement.Controllers
{
    public class ProjectController : BaseController
    {        
        private readonly IProjectLogic _projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            _projectLogic = projectLogic;
        }

        public ActionResult Index()
        {
            ProjectListViewModel projectListViewModel = this.GetProjectListViewModel("", 1);

            return View("ProjectList", projectListViewModel);
        }
        public ActionResult Query(ProjectListViewModel model, int? pageIndex)
        {
            model = this.GetProjectListViewModel(model.whereCondition, pageIndex);

            return View("ProjectList", model);
        }
        public ActionResult Create(ProjectViewModel model)
        {
            Project project = new Project();
            //BugManagement.Common.ConvertModel.ConvertMoudle(model, project);
            common.ConvertProjectViewModelToProject(model,project);
            if (model.ProjectId == 0)
            {
                _projectLogic.Create(project);
            }
            else
            {
                _projectLogic.Edit(project);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetProject(int id)
        {
            var project = new Project();
            if (id != 0)
            {
                project = _projectLogic.Get(id);
            }
            return Json(project, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(string projectId)
        {
            if (projectId != null && projectId != "")
            {
                _projectLogic.Delete(Convert.ToInt32(projectId));
            }
            return RedirectToAction("Index");
        }
        private ProjectListViewModel GetProjectListViewModel(string whereCondition, int? page)
        {
            ProjectListViewModel projectListViewModel = new ProjectListViewModel();
            List<ProjectViewModel> listmodel = null;
            ProjectViewModel projectmodel;
            PagingInfo<ProjectViewModel> projectPaging = null;

            var projectList = _projectLogic.GetProjectByWhereCondition(whereCondition);
            if (projectList != null && projectList.Count() > 0)
            {
                listmodel = new List<ProjectViewModel>();
                foreach (var project in projectList)
                {
                    projectmodel = new ProjectViewModel();
                    //BugManagement.Common.ConvertModel.ConvertMoudle(project, projectmodel);
                    common.ConvertProjectToProjectViewModel(project,projectmodel);
                    listmodel.Add(projectmodel);
                }
                projectPaging = new PagingInfo<ProjectViewModel>(PageSize, listmodel);
                projectPaging.PageIndex = page ?? 1;
                projectListViewModel.modelList = projectPaging.GetPagingData();

            }

            projectListViewModel.pagingInfo = projectPaging;
            projectListViewModel.model = new ProjectViewModel();

            return projectListViewModel;
        }
    }
}
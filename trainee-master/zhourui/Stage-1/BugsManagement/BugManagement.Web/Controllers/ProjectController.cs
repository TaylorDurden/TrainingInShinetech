using System.Web.Mvc;
using BugManagement.Data.Models;
using BugManagement.ILogic;

namespace BugManagement.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectLogic _projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            _projectLogic = projectLogic;
        }

        public ActionResult Index()
        {
            return View(_projectLogic.GetAll());
        }

        public ActionResult Create(Project project)
        {
            _projectLogic.Create(project);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _projectLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(Project project)
        {
            _projectLogic.Edit(project);
            return RedirectToAction("Index");
        }

        
    }
}
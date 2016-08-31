using System.Web.Mvc;
using BugManagement.Data.Models;
using BugManagement.ILogic;

namespace BugManagement.Web.Controllers
{
    [Authorize]
    public class DeveloperController : Controller
    {
        private readonly IDeveloperLogic _developerLogic;

        public DeveloperController(IDeveloperLogic developerLogic)
        {
            _developerLogic = developerLogic;
        }

        public ActionResult Index()
        {
            return View(_developerLogic.GetAll());
        }

        public ActionResult Create(Developer developer)
        {
            _developerLogic.Create(developer);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _developerLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(Developer developer)
        {
            _developerLogic.Edit(developer);
            return RedirectToAction("Index");
        }
    }
}
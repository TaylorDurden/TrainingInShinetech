using System.Web.Mvc;
using BugManagement.Data.Models;
using BugManagement.ILogic;

namespace BugManagement.Web.Controllers
{
    [Authorize]
    public class BugController : Controller
    {
        private readonly IBugLogic _bugLogic;

        public BugController(IBugLogic bugLogic)
        {
            _bugLogic = bugLogic;
        }

        public ActionResult Index()
        {
            return View(_bugLogic.GetAll());
        }

        public ActionResult CreateBug(Bug bug)
        {
            _bugLogic.Create(bug);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBug(int id)
        {
            _bugLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateBug(Bug bug)
        {
            _bugLogic.Edit(bug);
            return RedirectToAction("Index");
        }
    }
}
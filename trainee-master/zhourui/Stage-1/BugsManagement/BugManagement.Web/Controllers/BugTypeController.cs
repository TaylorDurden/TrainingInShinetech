using System.Web.Mvc;
using BugManagement.Data.Models;
using BugManagement.ILogic;

namespace BugManagement.Web.Controllers
{
    public class BugTypeController : Controller
    {
        private readonly IBugTypeLogic _bugTypeLogic;

        public BugTypeController(IBugTypeLogic bugTypeLogic)
        {
            _bugTypeLogic = bugTypeLogic;
        }
        
        public ActionResult Index()
        {
            return View(_bugTypeLogic.GetAll());
        }

        public ActionResult Create(BugType bugType)
        {
            _bugTypeLogic.Create(bugType);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _bugTypeLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(BugType bugType)
        {
            _bugTypeLogic.Edit(bugType);
            return RedirectToAction("Index");
        }
    }
}
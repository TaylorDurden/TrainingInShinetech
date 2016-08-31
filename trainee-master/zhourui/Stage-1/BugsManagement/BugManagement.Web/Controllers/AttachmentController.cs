using System.Web.Mvc;
using BugManagement.Data.Models;
using BugManagement.ILogic;

namespace BugManagement.Web.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly IAttachmentLogic _attachmentLogic;

        public AttachmentController(IAttachmentLogic attachmentLogic)
        {
            _attachmentLogic = attachmentLogic;
        }

        public ActionResult Index()
        {
            return View(_attachmentLogic.GetAll());
        }

        public ActionResult Create(Attachment attachment)
        {
            _attachmentLogic.Create(attachment);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _attachmentLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(Attachment attachment)
        {
            _attachmentLogic.Edit(attachment);
            return RedirectToAction("Index");
        }
    }
}
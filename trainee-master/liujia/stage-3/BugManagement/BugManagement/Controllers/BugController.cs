using BugManagement.DAL.Model;
using BugManagement.Models;
using BugManagement.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugManagement.Logic.ILogic;

namespace BugManagement.Controllers
{
    public class BugController : BaseController
    {
        private readonly IBugLogic _bugLogic;
        private readonly IDeveloperLogic _developerLogic;
        private readonly IProjectLogic _projectLogic;
        private readonly IBugTypeLogic _bugTypeLogic;
        private readonly ICauseBugDeveloperLogic _causeBugDeveloperLogic;
        private readonly IDocumentLogic _documentLogic;

        public BugController(IBugLogic bugLogic, IDeveloperLogic developerLogic, IProjectLogic projectLogic, IBugTypeLogic bugTypeLogic, ICauseBugDeveloperLogic causeBugDeveloperLogic, IDocumentLogic documentLogic)
        {
            _bugLogic = bugLogic;
            _developerLogic = developerLogic;
            _projectLogic = projectLogic;
            _bugTypeLogic = bugTypeLogic;
            _causeBugDeveloperLogic = causeBugDeveloperLogic;
            _documentLogic = documentLogic;
        }

        public ActionResult Index()
        {
            InitDropDownList();

            BugListViewModel bugListViewModel = this.GetBugListViewModel(string.Empty, 1);

            return View("BugList", bugListViewModel);
        }
        public ActionResult Query(BugListViewModel model, int? pageIndex)
        {
            InitDropDownList();

            model = this.GetBugListViewModel(model.whereCondition, pageIndex);

            return View("BugList", model);
        }
        public ActionResult Create(BugViewModel model, string strPage)
        {
            Bug bug = new Bug();

            //BugManagement.Common.ConvertModel.ConvertMoudle(model, bug);
            common.ConvertBugViewModelToBug(model,bug);
            if (model.BugId == 0)
            {
                _bugLogic.Create(bug, model.strDevelopers, model.strDocuments);
            }
            else
            {
                _bugLogic.Edit(bug, model.strDevelopers, model.strDocuments);
            }
            if (!string.IsNullOrEmpty(strPage) && strPage.Equals("Dashboard"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetBug(int id)
        {
            var bug = new Bug();
            if (id != 0)
            {
                bug = _bugLogic.Get(id);
            }
            return Json(bug, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBugDeveloper(int id)
        {
            List<Developer> developers = null;
            IEnumerable<CauseBugDeveloper> causeBugDevelopers;
            if (id != 0)
            {
                causeBugDevelopers = _causeBugDeveloperLogic.GetCauseBugDeveloperByBugId(id);
                if (causeBugDevelopers != null && causeBugDevelopers.Count() > 0)
                {
                    developers = new List<Developer>();
                    foreach (var bugDeveloper in causeBugDevelopers.ToList())
                    {
                        var developer = _developerLogic.Get(bugDeveloper.DeveloperId);
                        developers.Add(developer);
                    }
                }
            }
            return Json(developers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBugDocument(int id)
        {
            List<Document> documents = null;
            if (id != 0)
            {
                documents = _documentLogic.GetDocumentByBugId(id).ToList();
            }
            return Json(documents, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateBugStatus(string bugId, string stauts)
        {
            var result = "";
            if (!string.IsNullOrEmpty(bugId) && !string.IsNullOrEmpty(stauts))
            {
                try
                {
                    _bugLogic.UpdateBugStatus(Convert.ToInt32(bugId), stauts);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else
            {
                result = "Error";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Upload(HttpPostedFileBase Filedata)
        {
            if (Filedata == null ||
                string.IsNullOrEmpty(Filedata.FileName) ||
                Filedata.ContentLength == 0)
            {
                return Json(new { Error = this.HttpNotFound() });
            }

            string filename = System.IO.Path.GetFileName(Filedata.FileName);
            string virtualPath =
                string.Format("~/photos/{0}", filename);

            string path = this.Server.MapPath(virtualPath);

            Filedata.SaveAs(path);
            return Json(new { path = path });
        }
        private BugListViewModel GetBugListViewModel(string whereCondition, int? page)
        {
            BugListViewModel bugListViewModel = new BugListViewModel();
            List<BugViewModel> listmodel = null;
            BugViewModel bugmodel;
            PagingInfo<BugViewModel> bugPaging = null;

            var bugList = _bugLogic.GetBugByWhereCondition(whereCondition);
            if (bugList != null && bugList.Count() > 0)
            {
                listmodel = new List<BugViewModel>();
                foreach (var bug in bugList)
                {
                    bugmodel = new BugViewModel();
                    //BugManagement.Common.ConvertModel.ConvertMoudle(bug, bugmodel);
                    common.ConvertBugToBugViewModel(bug,bugmodel);
                    listmodel.Add(bugmodel);
                }
                bugPaging = new PagingInfo<BugViewModel>(PageSize, listmodel);
                bugPaging.PageIndex = page ?? 1;
                bugListViewModel.modelList = bugPaging.GetPagingData();
            }

            bugListViewModel.pagingInfo = bugPaging;
            bugListViewModel.model = new BugViewModel();

            return bugListViewModel;
        }
        private void InitDropDownList()
        {
            List<SelectListItem> typeList = new List<SelectListItem>();
            var typedbList = _bugTypeLogic.GetAll().ToList();
            if (typedbList != null && typedbList.Count() > 0)
            {
                foreach (var type in typedbList)
                {
                    typeList.Add(new SelectListItem() { Text = type.Name, Value = type.BugTypeId.ToString() });
                }
            }
            typeList.Insert(0, new SelectListItem() { Text = "select type", Value = "" });
            ViewBag.TypeList = typeList;

            List<SelectListItem> projectList = new List<SelectListItem>();
            List<Project> projectdbList = _projectLogic.GetAll().ToList();
            if (projectdbList != null && projectdbList.Count() > 0)
            {
                foreach (var project in projectdbList)
                {
                    projectList.Add(new SelectListItem() { Text = project.ProjectName, Value = project.ProjectId.ToString() });
                }
            }
            projectList.Insert(0, new SelectListItem() { Text = "select projects", Value = "" });
            ViewBag.ProjectList = projectList;

            List<SelectListItem> developerList = new List<SelectListItem>();
            List<Developer> developerdbList = _developerLogic.GetAll().ToList();
            if (developerdbList != null && developerdbList.Count() > 0)
            {
                foreach (var develoer in developerdbList)
                {
                    developerList.Add(new SelectListItem() { Text = develoer.FristName + " " + develoer.LastName, Value = develoer.DeveloperId.ToString() });
                }
            }
            ViewBag.DeveloperList = developerList;

            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem() { Text = "New", Value = "New" });
            statusList.Add(new SelectListItem() { Text = "Assigned", Value = "Assigned" });
            statusList.Add(new SelectListItem() { Text = "InProgress", Value = "InProgress" });
            statusList.Add(new SelectListItem() { Text = "InTest", Value = "InTest" });
            statusList.Add(new SelectListItem() { Text = "Done", Value = "Done" });
            ViewBag.StatusList = statusList;
        }
    }
}
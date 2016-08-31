using System.Web.Mvc;
using WebApplication1.Dal;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeInfoController : Controller
    {

        private readonly EmployeeInfoDal _objDs;
        public EmployeeInfoController()
        {
            _objDs = new EmployeeInfoDal();
        }
        // 
        // GET: /EmployeeInfo/ 
        public ActionResult Index()
        {
            var employees = _objDs.GetEmployees();
            return View(employees);
        }
        // 
        // GET: /EmployeeInfo/Details/5
        public ActionResult Details(int id)
        {
            var emp = _objDs.GetEmployeeById(id);
            return View(emp);
        }
        // 
        // GET: /EmployeeInfo/Create 
        public ActionResult Create()
        {
            var emp = new EmployeeInfo();
            return View(emp);
        }
        // 
        // POST: /EmployeeInfo/Create 
        [HttpPost]
        public ActionResult Create(EmployeeInfo emp)
        {

            _objDs.CreateEmployee(emp);
            return RedirectToAction("Index");

        }
        // 
        // GET: /EmployeeInfo/Edit/5 
        public ActionResult Edit(int id)
        {
            var emp = _objDs.GetEmployeeById(id);
            return View(emp);
        }
        // 
        // POST: /EmployeeInfo/Edit/5 
        [HttpPost]
        public ActionResult Edit(int id, EmployeeInfo emp)
        {

            _objDs.UpdateEmployee(emp);
            return RedirectToAction("Index");

        }
        // 
        // GET: /EmployeeInfo/Delete/5 
        public ActionResult Delete(int id)
        {
            var emp = _objDs.GetEmployeeById(id);
            return View(emp);
        }
        // 
        // POST: /EmployeeInfo/Delete/5 
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            var emp = _objDs.GetEmployeeById(id);
            _objDs.DeleteEmployee(emp);
            return RedirectToAction("Index");

        }

    }
}
using System;
using System.Web.Mvc;
using System.Web.Security;
using BugManagement.ILogic;
using BugManagement.Infrastructure;

namespace BugManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            string outmessage;
            if (_userLogic.Login(email, password, out outmessage))
            {
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                Console.WriteLine(outmessage);
                return Redirect("/");
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
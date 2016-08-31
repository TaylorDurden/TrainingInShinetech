using BugManagement.DAL.Model;
using BugManagement.Logic;
using BugManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;
using BugManagement.Logic.ILogic;
using BugManagement;
using BugManagement.Common;

namespace BugManagement.Controllers
{
    public class LoginController : Controller
    {
        public readonly IUserLogic _userLogic;
        public readonly ConvertModelToViewModel common = new ConvertModelToViewModel();
        
        public LoginController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }
        
        [AllowAnonymous]
        public ActionResult Signin(string returnUrl)
        {
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    return View("Error", new string[] { "You have logged in！" });
            //}
            //ViewBag.returnUrl = returnUrl;
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Signin(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userLogic.GetUserByEmailAndPassword(model.Email, model.Password);
                if (user == null)
                {
                    TempData[Constant.ErrorMessage] = "User Email or password Error!";                   
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    Session[Constant.Session_User] = user;
                    _userLogic.UpdateUserLastLoginTime(user.UserId);

                    return RedirectToAction(Constant.Action_Index, Constant.Controller_Dashboard);
                }                
            }
            ViewBag.returnUrl = returnUrl;

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            User user;
            if (ModelState.IsValid)
            {
                user = new User();
                //BugManagement.Common.ConvertModelToViewModel.ConvertMoudle(model, user);
                common.ConvertRegisterViewModelToUser(model,user);
                user.Type = "type2";
                user.RegisterTime = DateTime.Now;
                user.Status = "status2"; 

                _userLogic.Create(user);

                return RedirectToAction(Constant.Action_Signin);
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(Constant.Action_Signin);
        }
    }
}
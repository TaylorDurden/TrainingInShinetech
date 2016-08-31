using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugManagement;

namespace BugManagement.Controllers
{
    public class BaseController : Controller
    {
        public const int PageSize = 3;
        public ConvertModelToViewModel common = new ConvertModelToViewModel();

        public BaseController()
        {
        }

        public string GetLoginUserId()
        {
            string UserId = null;
            if (Session["user"] != null)
            {
                User loginUser = (User)Session["user"];
                UserId = loginUser.UserId.ToString();
            }
            return UserId ?? "";
        }
        public string GetLoginPassword()
        {
            string Password = null;
            if (Session["logInfo"] != null)
            {
                User loginUser = (User)Session["user"];
                Password = loginUser.Password;
            }
            return Password ?? "";
        }

        public string GetLoginUserName()
        {
            string UserName = null;
            if (Session["logInfo"] != null)
            {
                User loginUser = (User)Session["user"];
                UserName = loginUser.LastName + " " +loginUser.LastName;
            }
            return UserName ?? "";
        }

    }
}
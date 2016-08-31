using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugManagement.ILogic;

namespace BugManagement.Web.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly IUserLogic _userLogic;

        public ControllerBase(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }
    }
}
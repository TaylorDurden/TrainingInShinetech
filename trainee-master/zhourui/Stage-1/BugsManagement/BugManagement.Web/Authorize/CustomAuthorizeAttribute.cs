using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugManagement.Data.Models;
using BugManagement.ILogic;

namespace BugManagement.Web.Authorize
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IUserLogic _userLogic;

        public CustomAuthorizeAttribute(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public new string[] Roles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var role = "";
            switch (_userLogic.GetUserType(httpContext.User.Identity.Name))
            {
                case User.UserType.Admin:
                    role = "Admin";
                    break;
                case User.UserType.QA:
                    role = "QA";
                    break;
                case User.UserType.Developer:
                    role = "Developer";
                    break;
                case User.UserType.Anonymous:
                    break;
                default:
                    break;
            }

            return Roles.Contains(role) || base.AuthorizeCore(httpContext);
        }
    }
}
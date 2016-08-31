using System.Web;
using System.Web.Mvc;
using BugManagement.ILogic;
using BugManagement.Web.Authorize;

namespace BugManagement.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}

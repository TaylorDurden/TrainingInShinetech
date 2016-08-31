
using System;
using System.Collections.Generic;

namespace BugManagement.Common
{
    public static class Constant
    {
        #region session
        public static readonly string SessionUser = "user";
        #endregion

        #region other
        public static readonly string ErrorMessage = "ErrorMessage";
        #endregion

        #region action
        public static readonly string ActionIndex = "Index";
        public static readonly string ActionSignin = "Signin";
        #endregion

        #region Controller
        public static readonly string ControllerDashboard = "Dashboard";
        #endregion

        #region Page
        public static readonly int PageSize = 3;
        #endregion

        public static List<int> GetPages(int pageSize,int count)
        {
            var pages = new List<int>();
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageCount > 1)
            {
                for (var index = 1; index <= pageCount; index++)
                {
                    pages.Add(index);
                }
            }
            return pages;
        }

    }
}

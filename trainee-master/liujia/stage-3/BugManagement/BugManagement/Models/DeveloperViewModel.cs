using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugManagement.Models
{
    public class DeveloperListViewModel
    {
        public string whereCondition { get; set; }
        public IEnumerable<DeveloperViewModel> modelList { get; set; }
        public DeveloperViewModel model { get; set; }
        public PagingInfo<DeveloperViewModel> pagingInfo { get; set; }        
    }
    public class DeveloperViewModel
    {
        public int DeveloperId { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }
    }
}
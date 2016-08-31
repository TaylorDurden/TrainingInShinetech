using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugManagement.Models
{
    public class DashboardViewModel
    {
        public string whereCondition { get; set; }
        public List<BugViewModel> assignedBugList { get; set; }
        public List<BugViewModel> inProgressBugList { get; set; }
        public List<BugViewModel> inTestBugList { get; set; }
        public List<BugViewModel> doneBugList { get; set; }
    }
    
}
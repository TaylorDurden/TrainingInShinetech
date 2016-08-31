using System.Collections.Generic;

namespace BugManagemnet.WebAPI.Models
{
    public class DashboardViewModel
    {
        public List<BugViewModel> AssignedBugList { get; set; }
        public List<BugViewModel> InProgressBugList { get; set; }
        public List<BugViewModel> InTestBugList { get; set; }
        public List<BugViewModel> DoneBugList { get; set; }
    }
}
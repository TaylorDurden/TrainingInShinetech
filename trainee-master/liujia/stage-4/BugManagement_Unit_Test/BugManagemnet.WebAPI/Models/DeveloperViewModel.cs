using System.Collections.Generic;

namespace BugManagemnet.WebAPI.Models
{
    public class DeveloperListViewModel
    {
        public List<DeveloperViewModel> Models { get; set; }
        public List<int> Pages { get; set; }
    }

    public class DeveloperViewModel
    {
        public int DeveloperId { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
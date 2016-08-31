using System;
using System.Collections.Generic;

namespace BugManagemnet.WebAPI.Models
{
    public class BugListViewModel
    {
        public List<BugViewModel> Models { get; set; }
        public List<int> Pages { get; set; }
    }

    public class BugCommand
    {
        public BugViewModel BugViewModel { get; set; }
        public List<DeveloperViewModel> DeveloperViewModels { get; set; }
    }

    public class BugViewModel
    {
        public int BugId { get; set; }
        public string Title { get; set; }
        public string Smmary { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Type { get; set; }
        public string Creator { get; set; }
        public DateTime? Createtime { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string StrDocuments { get; set; }
        public string StrDevelopers { get; set; }
        public int DeveloperId { get; set; }
    }
}
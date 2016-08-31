using System;
using System.Collections.Generic;

namespace BugManagemnet.WebAPI.Models
{
    public class ProjectListViewModel
    {
        public List<ProjectViewModel> ModelList { get; set; }
        public int ModelCount { get; set; }

    }
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string MainContact { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
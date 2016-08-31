using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugManagement.Models
{
    public class ProjectListViewModel
    {
        public string whereCondition { get; set; }
        public ProjectViewModel model { get; set; }
        public IEnumerable<ProjectViewModel> modelList { get; set; }        
        public PagingInfo<ProjectViewModel> pagingInfo { get; set; }

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
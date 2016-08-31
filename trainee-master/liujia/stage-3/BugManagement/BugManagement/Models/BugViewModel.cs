using BugManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugManagement.Models
{
    public class BugListViewModel
    {
        public string whereCondition { get; set; }
        public IEnumerable<BugViewModel> modelList { get; set; }
        public BugViewModel model { get; set; }
        public PagingInfo<BugViewModel> pagingInfo { get; set; }  
    }

    public class BugViewModel
    {
        public int BugId { get; set; }

        public string Title { get; set; }

        public string Smmary { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public string Creator { get; set; }

        public DateTime? Createtime { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public string strDocuments { get; set; }
        public string strDevelopers { get; set; }

        public int DeveloperId { get; set; }              
    }
}
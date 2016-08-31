using System;
using BugManagement.DAL.Model;

namespace BugManagement.Logic.Models
{
    public class BugLogicModel
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
        
        public string StrDocuments { get; set; }
        public string StrDevelopers { get; set; }

        public int DeveloperId { get; set; }

        public Project Project { get; set; }
    }
}

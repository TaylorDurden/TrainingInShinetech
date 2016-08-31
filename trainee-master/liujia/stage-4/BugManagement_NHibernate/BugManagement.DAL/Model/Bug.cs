using System;
using System.Collections.Generic;

namespace BugManagement.DAL.Model
{
    public class Bug
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

        public virtual Project Project { get; set; }

        public virtual ICollection<CauseBugDeveloper> CauseBugDevelopers { get; set; }
        
        public virtual ICollection<Document> Documents { get; set; }
    }
}

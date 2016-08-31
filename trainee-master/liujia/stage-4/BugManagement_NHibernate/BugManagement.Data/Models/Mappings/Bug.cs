using System;
using System.Collections.Generic;

namespace BugManagement.Data.Models.Mappings
{
    public class Bug
    {
        public virtual int BugId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Smmary { get; set; }

        public virtual string Description { get; set; }

        public virtual string Status { get; set; }

        public virtual int Type { get; set; }

        public virtual string Creator { get; set; }

        public virtual DateTime? Createtime { get; set; }
        
        public virtual Project Project { get; set; }

        public virtual ICollection<CauseBugDeveloper> CauseBugDevelopers { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}

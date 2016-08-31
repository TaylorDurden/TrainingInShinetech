
using System;
using System.Collections.Generic;

namespace BugManagement.Data.Models.Mappings
{
    public class Project
    {
        public virtual int ProjectId { get; set; }

        public virtual string ProjectName { get; set; }

        public virtual string Description { get; set; }

        public virtual string MainContact { get; set; }

        public virtual string ContactEmail { get; set; }

        public virtual DateTime? StartTime { get; set; }

        public virtual ICollection<Bug> Bugs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugManagement.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainContact { get; set; }
        public string ContactEmail { get; set; }
        public DateTime StarTime { get; set; }

        public virtual ICollection<Bug> Bugs { get; set; }
    }
}

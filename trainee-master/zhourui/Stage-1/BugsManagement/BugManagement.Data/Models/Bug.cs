using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugManagement.Data.Models
{
    public class Bug
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public BugType Type { get; set; }
        public virtual Project Project { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public BugStatus Status { get; set; }

        public virtual ICollection<Developer> Developers { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }

        public enum BugStatus
        {
            New = 0,
            Assigned = 1,
            InProgess = 2,
            InTest = 3,
            Done = 4
        };
    }
}

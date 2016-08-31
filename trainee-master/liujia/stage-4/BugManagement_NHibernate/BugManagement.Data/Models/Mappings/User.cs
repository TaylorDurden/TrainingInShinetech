using System;
using System.Collections.Generic;

namespace BugManagement.Data.Models.Mappings
{
    public class User
    {
        public virtual int UserId { get; set; }

        public virtual string FristName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string Type { get; set; }

        public virtual DateTime? RegisterTime { get; set; }

        public virtual DateTime? LastLoginTime { get; set; }

        public virtual string Status { get; set; }

        public virtual ICollection<Developer> Developers { get; set; }
    }
}

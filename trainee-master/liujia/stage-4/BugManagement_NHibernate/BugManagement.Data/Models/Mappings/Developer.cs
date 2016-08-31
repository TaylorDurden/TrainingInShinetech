using System.Collections.Generic;

namespace BugManagement.Data.Models.Mappings
{
    public class Developer
    {
        public virtual int DeveloperId { get; set; }

        public virtual string FristName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual string Status { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CauseBugDeveloper> CauseBugDevelopers { get; set; }
    }
}

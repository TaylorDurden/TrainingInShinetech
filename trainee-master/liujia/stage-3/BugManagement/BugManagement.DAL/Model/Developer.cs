using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.DAL.Model
{
    public class Developer
    {
        public int DeveloperId { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        //public virtual User User { get; set; }

        //public virtual ICollection<CauseBugDeveloper> CauseBugDevelopers { get; set; }
    }
}

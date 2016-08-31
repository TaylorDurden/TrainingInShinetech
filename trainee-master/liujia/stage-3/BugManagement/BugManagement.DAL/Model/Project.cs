using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.DAL.Model
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public string MainContact { get; set; }

        public string ContactEmail { get; set; }

        public DateTime? StartTime { get; set; }
    }
}

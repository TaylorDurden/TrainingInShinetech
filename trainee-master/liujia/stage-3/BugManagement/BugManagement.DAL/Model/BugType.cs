using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.DAL.Model
{
    public class BugType
    {
        public int BugTypeId { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EmployeeInfo
    {
        public virtual int EmpNo { get; set; }

        public virtual string EmpName { get; set; }

        public virtual int Salary { get; set; }

        public virtual string DeptName { get; set; }

        public virtual string Designation { get; set; }
    }
}
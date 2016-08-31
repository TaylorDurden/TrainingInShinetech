using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanPoker.ILogic.Models;

namespace PlanPoker.WebAPI.Models
{
    public class ProjectViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProjectGuid { get; set; }
    }
}
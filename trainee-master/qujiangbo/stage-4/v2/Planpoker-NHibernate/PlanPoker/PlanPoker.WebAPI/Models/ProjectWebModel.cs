using System;

namespace PlanPoker.WebAPI.Models
{
    public class ProjectWebModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectGuid { get; set; }
    }
}
using System;

namespace PlanPoker.Data.Models
{
    public class Project
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual Guid ProjectGuid { get; set; }
    }
}

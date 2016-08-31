using System;
using System.ComponentModel.DataAnnotations;

namespace PlanPoker.Data.Models
{
    public class Project
    {
        //[Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual Guid ProjectGuid { get; set; }

        public virtual DateTime StartDate { get; set; }
    }
}

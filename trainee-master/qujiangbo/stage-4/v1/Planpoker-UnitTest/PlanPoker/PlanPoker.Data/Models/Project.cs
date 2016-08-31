using System;
using System.ComponentModel.DataAnnotations;

namespace PlanPoker.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid ProjectGuid { get; set; }
    }
}

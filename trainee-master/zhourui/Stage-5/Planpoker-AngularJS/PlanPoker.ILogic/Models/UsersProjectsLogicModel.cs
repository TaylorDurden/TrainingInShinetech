using System;
using System.Collections.Generic;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.Models
{
    public class UsersProjectsLogicModel
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
    }
}

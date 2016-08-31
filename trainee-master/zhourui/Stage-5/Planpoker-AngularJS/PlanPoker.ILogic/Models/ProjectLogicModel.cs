using System.Collections.Generic;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.Models
{
    public class ProjectLogicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }

        public string Message { get; set; }

        public virtual ICollection<User> InvitedUsers { get; set; }
    }

    public static class ProjectConverter
    {
        public static Project ConvertToProjectEntity(this ProjectLogicModel model)
        {
            return new Project
            {
                Id = model.Id,
                Name = model.Name,
                InvitedUsers = model.InvitedUsers
            };
        }

        public static ProjectLogicModel ConvertToProjectLogicModel(this Project project, string message)
        {
            return new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                Message = message,
                InvitedUsers = project.InvitedUsers
            };
        }
    }
}
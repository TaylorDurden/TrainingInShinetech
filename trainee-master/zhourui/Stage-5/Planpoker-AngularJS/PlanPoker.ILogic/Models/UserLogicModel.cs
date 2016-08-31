using System.Collections.Generic;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.Models
{
    public class UserLogicModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

        public string Message { get; set; }

        public virtual ICollection<Project> OwnedProjects { get; set; }
        public virtual ICollection<Project> RelatedProjects { get; set; }
    }

    public static class UserConverter
    {
        public static User ConvertToUserEntity(this UserLogicModel model)
        {
            return new User
            {
                UserId = model.UserId,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Image = model.Image,
                RelatedProjects = model.RelatedProjects
            };
        }

        public static UserLogicModel ConvertToUserLogicModel(this User user, string message)
        {
            return new UserLogicModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image,
                Message = message,
                RelatedProjects = user.RelatedProjects
            };
        }
    }
}
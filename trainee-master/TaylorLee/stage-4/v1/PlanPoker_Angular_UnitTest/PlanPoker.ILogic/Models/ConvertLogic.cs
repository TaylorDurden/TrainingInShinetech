using System;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.Models
{
    public static class ConvertLogic
    {
        public static Project ConvertToModel(this ProjectLogicModel model)
        {
            return new Project
            {
                Id = int.Parse(model.Id ?? "0"),
                Name = model.Name,
                ProjectGuid = Guid.Parse(model.ProjectGuid)
            };
        }

        public static ProjectLogicModel ConvertToLogicModel(this Project project)
        {
            return new ProjectLogicModel
            {
                Id = project.Id.ToString(),
                Name = project.Name,
                ProjectGuid = project.ProjectGuid.ToString()
            };
        }

        public static User ConvertToModel(this UserLogicModel model)
        {
            return new User
            {
                UserId = int.Parse(model.UserId ?? "0"),
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Image = model.Image,
            };
        }

        public static UserLogicModel ConvertToLogicModel(this User user)
        {
            return new UserLogicModel {
                UserId = user.UserId.ToString(),
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image
            };
        }
    }
}
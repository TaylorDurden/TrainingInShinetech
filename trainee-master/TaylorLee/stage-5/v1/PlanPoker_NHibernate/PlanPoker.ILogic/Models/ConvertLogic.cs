using System;
using System.Globalization;

namespace PlanPoker.ILogic.Models
{
    public static class ConvertLogic
    {
        public static Data.Models.Project ConvertToModel(this ProjectLogicModel model)
        {
            return new Data.Models.Project
            {
                Id = int.Parse(model.Id ?? "0"),
                Name = model.Name,
                ProjectGuid = Guid.Parse(model.ProjectGuid),
                StartDate = DateTime.Parse(model.StartDate)
            };
        }

        public static ProjectLogicModel ConvertToLogicModel(this Data.Models.Project project)
        {
            return new ProjectLogicModel
            {
                Id = project.Id.ToString(),
                Name = project.Name,
                ProjectGuid = project.ProjectGuid.ToString(),
                StartDate = project.StartDate.ToString()
            };
        }

        public static Data.Models.User ConvertToModel(this UserLogicModel model)
        {
            return new Data.Models.User
            {
                UserId = int.Parse(model.UserId ?? "0"),
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Image = model.Image,
            };
        }

        public static UserLogicModel ConvertToLogicModel(this Data.Models.User user)
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
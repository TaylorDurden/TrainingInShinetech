using PlanPoker.ILogic.LogicModel;

namespace PlanPoker.WebAPI.Models
{
    public static class Convert
    {
        public static UserLogicModel ConvertToLogicModel(this UserViewModel model)
        {
            return new UserLogicModel
            {
                UserId = model.UserId,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Image = model.Image
            };

        }

        public static UserViewModel ConvertToViewModel(this UserLogicModel user)
        {
            return new UserViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image,
            };
        }

        public static ProjectLogicModel ConvertToProjectLogicModel(this ProjectViewModel model)
        {
            return new ProjectLogicModel
            {
                Id = model.Id,
                Name = model.Name,
                ProjectGuid = model.ProjectGuid
            };
        }

        public static ProjectViewModel ConvertToProjectViewModel(this ProjectLogicModel model)
        {
            return new ProjectViewModel
            {
                Id = model.Id,
                Name = model.Name,
                ProjectGuid = model.ProjectGuid
            };
        }
    }
}
using PlanPoker.ILogic.Models;

namespace PlanPoker.WebAPI.Models
{
    public static class ModelConverter
    {
        public static UserViewModel ConvertToUserViewModel(this UserLogicModel model)
        {
            if (model == null) return null;

            return new UserViewModel
            {
                UserId = model.UserId,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Image = model.Image,
                Message = model.Message,
                Status = model.Status
            };
        }

        public static ProjectViewModel ConvertToProjectViewModel(this ProjectLogicModel model)
        {
            if (model == null) return null;

            return new ProjectViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Message = model.Message,
                Status = model.Status
            };
        }
    }
}
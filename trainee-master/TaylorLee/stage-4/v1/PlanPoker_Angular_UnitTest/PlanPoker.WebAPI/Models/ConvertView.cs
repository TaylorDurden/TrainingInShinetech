using PlanPoker.ILogic.Models;

namespace PlanPoker.WebAPI.Models
{
    public static class ConvertView
    {
        public static ProjectLogicModel ConvertToLogicModel(this ProjectViewModel projectViewModel)
        {
            return new ProjectLogicModel
            {
                Id = projectViewModel.Id,
                Name = projectViewModel.Name,
                ProjectGuid = projectViewModel.ProjectGuid
            };
        }

        public static ProjectViewModel ConvertToViewModel(this ProjectLogicModel projectLogicModel)
        {
            return new ProjectViewModel
            {
                Id = projectLogicModel.Id,
                Name = projectLogicModel.Name,
                ProjectGuid = projectLogicModel.ProjectGuid
            };
        }

        public static UserLogicModel ConvertToLogicModel(this UserViewModel userViewModel)
        {
            return new UserLogicModel
            {
                UserId = userViewModel.UserId,
                UserName = userViewModel.UserName,
                Password = userViewModel.Password,
                Email = userViewModel.Email,
                Image = userViewModel.Image,
            };
        }

        public static UserViewModel ConvertToViewModel(this UserLogicModel userLogicModel)
        {
            return new UserViewModel
            {
                UserId = userLogicModel.UserId,
                UserName = userLogicModel.UserName,
                Password = userLogicModel.Password,
                Email = userLogicModel.Email,
                Image = userLogicModel.Image,
            };
        }
    }
}
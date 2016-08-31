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
        public bool Status { get; set; }
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
                Image = model.Image
            };
        }

        public static UserLogicModel ConvertToUserLogicModel(this User user, string message, bool status)
        {
            return new UserLogicModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image,
                Message = message,
                Status = status
            };
        }
    }
}
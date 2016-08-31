using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.Models
{
    public static class UserLogicConvert
    {
        public static User CreateConvert(this UserLogicModel userLogicModel)
        {
            return new User
            {
                UserId = userLogicModel.UserId,
                UserName = userLogicModel.UserName,
                Password = userLogicModel.Password,
                Email = userLogicModel.Email,
                Image = userLogicModel.Image
            };

        }

        public static User EditConvert(this UserLogicModel userLogicModel)
        {
            return new User
            {
                UserId = userLogicModel.UserId,
                UserName = userLogicModel.UserName,
                Password = userLogicModel.Password,
                Email = userLogicModel.Email,
                Image = userLogicModel.Image
            };
        }


        public static UserLogicModel GetConvert(this User user)
        {
            return new UserLogicModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image
            };
        }

        public static UserLogicModel LoginConvert(this User user)
        {
            return new UserLogicModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image,
                Message = "",
                Status = true
            };
        }

        public static List<UserLogicModel> GetAllConvert(this List<User> users)
        {
            return users.Select(user => new UserLogicModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image
            }).ToList();
        }

        public static List<UserLogicModel> QueryByNameConvert(this List<User> users)
        {
            return users.Select(user => new UserLogicModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image
            }).ToList();
        }
    }
}

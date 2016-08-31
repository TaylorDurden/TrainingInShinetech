using System.Collections.Generic;
using System.Linq;
using PlanPoker.ILogic.Models;

namespace PlanPoker.WebAPI.Models
{
    public static class UserWebConvert
    {
        public static UserLogicModel CreateConvert(this UserWebModel userWebModel)
        {
            return new UserLogicModel
            {
                UserId = userWebModel.UserId,
                UserName = userWebModel.UserName,
                Password = userWebModel.Password,
                Email = userWebModel.Email,
                Image = userWebModel.Image
            };
        }

        public static UserLogicModel EditConvert(this UserWebModel userWebModel)
        {
            return new UserLogicModel
            {
                UserId = userWebModel.UserId,
                UserName = userWebModel.UserName,
                Password = userWebModel.Password,
                Email = userWebModel.Email,
                Image = userWebModel.Image
            };
        }

        public static UserWebModel GetUserByIdConvert(this UserLogicModel userLogicModel)
        {
            return new UserWebModel
            {
                UserId = userLogicModel.UserId,
                UserName = userLogicModel.UserName,
                Password = userLogicModel.Password,
                Email = userLogicModel.Email,
                Image = userLogicModel.Image
            };
        }

        public static List<UserWebModel> GetAllConvert(this List<UserLogicModel> usersLogic)
        {
            return usersLogic.Select(userLogic => new UserWebModel
            {
                UserId = userLogic.UserId,
                UserName = userLogic.UserName,
                Password = userLogic.Password,
                Email = userLogic.Email,
                Image = userLogic.Image
            }).ToList();
        }

        public static UserWebModel LoginConvert(this UserLogicModel userLogicModel)
        {
            return new UserWebModel
            {
                UserId = userLogicModel.UserId,
                UserName = userLogicModel.UserName,
                Password = userLogicModel.Password,
                Email = userLogicModel.Email,
                Image = userLogicModel.Image,
                Message =userLogicModel.Message,
                Status = userLogicModel.Status
            };
        }

        public static List<UserWebModel> QueryByNameConvert(this List<UserLogicModel> usersLogic)
        {
            return usersLogic.Select(userLogic => new UserWebModel
            {
                UserId = userLogic.UserId,
                UserName = userLogic.UserName,
                Password = userLogic.Password,
                Email = userLogic.Email,
                Image = userLogic.Image
            }).ToList();
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.LogicModel
{
    public static class Convert
    {
        public static UserLogicModel ConvertToUserLogicModel(this PlanPokerUser model)
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

        public static PlanPokerUser ConvertToUserEntity(this UserLogicModel user)
        {
            return new PlanPokerUser
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Image = user.Image,
            };
        }

        public static ProjectLogicModel ConvertToProjectLogicModel(this Project model)
        {
            return new ProjectLogicModel
            {
                Id = model.Id,
                Name = model.Name,
                ProjectGuid = model.ProjectGuid
            };
        }

        public static Project ConvertToProjectEntity(this ProjectLogicModel model)
        {
            return new Project
            {
                Id = model.Id,
                Name = model.Name,
                ProjectGuid = model.ProjectGuid
            };
        }
    }
}

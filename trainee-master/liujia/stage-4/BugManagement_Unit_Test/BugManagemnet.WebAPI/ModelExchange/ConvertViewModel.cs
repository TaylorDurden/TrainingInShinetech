using BugManagemnet.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.Models;

namespace BugManagemnet.WebAPI.ModelExchange
{
    public static class ConvertViewModel
    {
        public static BugViewModel ConvertToBugViewModel(this BugLogicModel bugLogicModel)
        {
            var strProjectName = "";
            if (!string.IsNullOrEmpty(bugLogicModel.Project?.ProjectName))
            {
                strProjectName = bugLogicModel.Project.ProjectName;
            }
            return new BugViewModel()
            {
                BugId = bugLogicModel.BugId,
                Title = bugLogicModel.Title,
                Smmary = bugLogicModel.Smmary,
                Description = bugLogicModel.Description,
                Status = bugLogicModel.Status,
                Type = bugLogicModel.Type,
                Creator = bugLogicModel.Creator,
                Createtime = bugLogicModel.Createtime,
                ProjectId = bugLogicModel.ProjectId,
                ProjectName = strProjectName
            };
        }
        public static BugLogicModel ConvertToBugLogicModel(this BugViewModel bugViewModel)
        {
            return new BugLogicModel()
            {
                BugId = bugViewModel.BugId,
                Title = bugViewModel.Title,
                Smmary = bugViewModel.Smmary,
                Description = bugViewModel.Description,
                Status = bugViewModel.Status,
                Type = bugViewModel.Type,
                Creator = bugViewModel.Creator,
                Createtime = bugViewModel.Createtime,
                ProjectId = bugViewModel.ProjectId
            };
        }
        public static UserViewModel ConvertToUserViewModel(this UserLogicModel userLogicModel)
        {
            return new UserViewModel()
            {
                UserId = userLogicModel.UserId,
                FristName = userLogicModel.FristName,
                LastName = userLogicModel.LastName,
                Email = userLogicModel.Email,
                Password = userLogicModel.Password,
                Type = userLogicModel.Type,
                RegisterTime = userLogicModel.RegisterTime,
                LastLoginTime = userLogicModel.LastLoginTime,
                Status = userLogicModel.Status
            };

        }
        public static UserLogicModel ConvertToUserLogicModel(this UserViewModel userViewModel)
        {
            return new UserLogicModel()
            {
                UserId = userViewModel.UserId,
                FristName = userViewModel.FristName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                Password = userViewModel.Password,
                Type = userViewModel.Type,
                RegisterTime = userViewModel.RegisterTime,
                LastLoginTime = userViewModel.LastLoginTime,
                Status = userViewModel.Status
            };

        }
        public static DeveloperViewModel ConvertToDeveloperViewModel(this DeveloperLogicModel developerLogicModel)
        {
            var strUserName = "";
            if (!string.IsNullOrEmpty(developerLogicModel.User?.FristName) && !string.IsNullOrEmpty(developerLogicModel.User.LastName))
            {
                strUserName = developerLogicModel.User.FristName + " " + developerLogicModel.User.LastName;
            }
            return new DeveloperViewModel()
            {
                DeveloperId = developerLogicModel.DeveloperId,
                FristName = developerLogicModel.FristName,
                LastName = developerLogicModel.LastName,
                Email = developerLogicModel.Email,
                Status = developerLogicModel.Status,
                UserId = developerLogicModel.UserId,
                UserName = strUserName
            };
        }
        public static DeveloperLogicModel ConvertToDeveloperLogicModel(this DeveloperViewModel developerViewModel)
        {
            return new DeveloperLogicModel()
            {
                DeveloperId = developerViewModel.DeveloperId,
                FristName = developerViewModel.FristName,
                LastName = developerViewModel.LastName,
                Email = developerViewModel.Email,
                Status = developerViewModel.Status,
                UserId = developerViewModel.UserId
            };
        }
        public static ProjectViewModel ConvertToProjectViewModel(this ProjectLogicModel projectLogicModel)
        {
            return new ProjectViewModel()
            {
                ProjectId = projectLogicModel.ProjectId,
                ProjectName = projectLogicModel.ProjectName,
                Description = projectLogicModel.Description,
                MainContact = projectLogicModel.MainContact,
                ContactEmail = projectLogicModel.ContactEmail,
                StartTime = projectLogicModel.StartTime
            };

        }
        public static ProjectLogicModel ConvertToProjectLogicModel(this ProjectViewModel projectViewModel)
        {
            return new ProjectLogicModel()
            {
                ProjectId = projectViewModel.ProjectId,
                ProjectName = projectViewModel.ProjectName,
                Description = projectViewModel.Description,
                MainContact = projectViewModel.MainContact,
                ContactEmail = projectViewModel.ContactEmail,
                StartTime = projectViewModel.StartTime
            };

        }
        public static UserLogicModel ConvertRegisterViewModelToUserLogicModel(this RegisterViewModel registerViewModel)
        {
            return new UserLogicModel()
            {
                FristName = registerViewModel.FristName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                LastLoginTime = DateTime.Now,
                Type = "Developer",
                RegisterTime = DateTime.Now,
                Status = "New"
            };
        }
        public static BugTypeViewModel ConvertToBugTypeViewModel(this BugTypeLogicModel bugTypeLogicModel)
        {
            return new BugTypeViewModel()
            {
                BugTypeId = bugTypeLogicModel.BugTypeId,
                Name = bugTypeLogicModel.Name,
                Status = bugTypeLogicModel.Status
            };
        }
        public static BugTypeLogicModel ConvertToBugTypeLogicModel(this BugTypeViewModel bugTypeViewModel)
        {
            return new BugTypeLogicModel()
            {
                BugTypeId = bugTypeViewModel.BugTypeId,
                Name = bugTypeViewModel.Name,
                Status = bugTypeViewModel.Status
            };
        }

        public static List<BugViewModel> ConvertToBugViewModels(this List<BugLogicModel> bugLogicModels)
        {
            return !bugLogicModels.Any()
                 ? null
                 : bugLogicModels.Select(n => n.ConvertToBugViewModel()).ToList();
        }
        public static List<DeveloperViewModel> ConvertToDeveloperViewModels(this List<DeveloperLogicModel> developerLogicModels)
        {
            return !developerLogicModels.Any()
                             ? null
                             : developerLogicModels.Select(n => n.ConvertToDeveloperViewModel()).ToList();
        }
        public static List<DeveloperLogicModel> ConvertToDeveloperViewModels(this List<DeveloperViewModel> developerViewModels)
        {
            return !developerViewModels.Any()
                             ? null
                             : developerViewModels.Select(n => n.ConvertToDeveloperLogicModel()).ToList();
        }
    }
}
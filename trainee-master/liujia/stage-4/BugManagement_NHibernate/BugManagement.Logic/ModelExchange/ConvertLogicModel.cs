using System.Collections.Generic;
using System.Linq;
using BugManagement.Data.Models.Mappings;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.ModelExchange
{
    public static class ConvertLogicModel
    {
        public static Bug ConvertToBug(this BugLogicModel bugLogicModel)
        {
            return new Bug()
            {
                BugId = bugLogicModel.BugId,
                Title = bugLogicModel.Title,
                Smmary = bugLogicModel.Smmary,
                Description = bugLogicModel.Description,
                Status = bugLogicModel.Status,
                Type = bugLogicModel.Type,
                Creator = bugLogicModel.Creator,
                Createtime = bugLogicModel.Createtime
            };
        }
        public static BugLogicModel ConvertToBugLogicModel(this Bug bug)
        {
            return new BugLogicModel()
            {
                BugId = bug.BugId,
                Title = bug.Title,
                Smmary = bug.Smmary,
                Description = bug.Description,
                Status = bug.Status,
                Type = bug.Type,
                Creator = bug.Creator,
                Createtime = bug.Createtime,
                Project = bug.Project,
                ProjectId = bug.Project.ProjectId
            };
        }
        public static User ConvertToUser(this UserLogicModel userLogicModel)
        {
            return new User()
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
        public static UserLogicModel ConvertToUserLogicModel(this User user)
        {
            return new UserLogicModel()
            {
                UserId = user.UserId,
                FristName = user.FristName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Type = user.Type,
                RegisterTime = user.RegisterTime,
                LastLoginTime = user.LastLoginTime,
                Status = user.Status
            };
        }
        public static Developer ConvertToDeveloper(this DeveloperLogicModel developerLogicModel)
        {
            return new Developer()
            {
                DeveloperId = developerLogicModel.DeveloperId,
                FristName = developerLogicModel.FristName,
                LastName = developerLogicModel.LastName,
                Email = developerLogicModel.Email,
                Status = developerLogicModel.Status
            };
        }
        public static DeveloperLogicModel ConvertToDeveloperLogicModel(this Developer developer)
        {
            return new DeveloperLogicModel()
            {
                DeveloperId = developer.DeveloperId,
                FristName = developer.FristName,
                LastName = developer.LastName,
                Email = developer.Email,
                Status = developer.Status,
                //UserId = developer.User.UserId,
                User = developer.User
            };
        }
        public static Project ConvertToProject(this ProjectLogicModel projectLogicModel)
        {
            return new Project()
            {
                ProjectId = projectLogicModel.ProjectId,
                ProjectName = projectLogicModel.ProjectName,
                Description = projectLogicModel.Description,
                MainContact = projectLogicModel.MainContact,
                ContactEmail = projectLogicModel.ContactEmail,
                StartTime = projectLogicModel.StartTime
            };
        }
        public static ProjectLogicModel ConvertToProjectLogicModel(this Project project)
        {
            return new ProjectLogicModel()
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Description = project.Description,
                MainContact = project.MainContact,
                ContactEmail = project.ContactEmail,
                StartTime = project.StartTime
            };
        }
        public static BugTypeLogicModel ConvertToBugTypeLogicModel(this BugType bugType)
        {
            return new BugTypeLogicModel()
            {
                BugTypeId = bugType.BugTypeId,
                Name = bugType.Name,
                Status = bugType.Status
            };
        }
        public static BugType ConvertToBugType(this BugTypeLogicModel bugTypeLogicModel)
        {
            return new BugType()
            {
                BugTypeId = bugTypeLogicModel.BugTypeId,
                Name = bugTypeLogicModel.Name,
                Status = bugTypeLogicModel.Status
            };
        }
        public static CauseBugDeveloperLogicModel ConvertToCauseBugDeveloperLogicModel(this CauseBugDeveloper causeBugDeveloper)
        {
            return new CauseBugDeveloperLogicModel()
            {
                BugId = causeBugDeveloper.Bug.BugId,
                DeveloperId = causeBugDeveloper.Developer.DeveloperId,
                Id = causeBugDeveloper.Id
            };
        }
        public static CauseBugDeveloper ConvertToCauseBugDeveloper(this CauseBugDeveloperLogicModel causeBugDeveloperLogicModel)
        {
            return new CauseBugDeveloper()
            {
                //BugId = causeBugDeveloperLogicModel.BugId,
                //DeveloperId = causeBugDeveloperLogicModel.DeveloperId,
                Id = causeBugDeveloperLogicModel.Id
            };
        }

        public static List<UserLogicModel> ConvertToUserLogicModels(this List<User> users)
        {
            return !users.Any()
                 ? null
                 : users.Select(n => n.ConvertToUserLogicModel()).ToList();
        }
        public static List<ProjectLogicModel> ConvertToProjectLogicModels(this List<Project> projects)
        {
            return !projects.Any()
                 ? null
                 : projects.Select(n => n.ConvertToProjectLogicModel()).ToList();
        }

        public static void ConvertUserLogicModelToDbUser(this UserLogicModel userLogicModel,User user)
        {
            user.UserId = userLogicModel.UserId;
            user.FristName = userLogicModel.FristName;
            user.LastName = userLogicModel.LastName;
            user.Email = userLogicModel.Email;
            user.Password = userLogicModel.Password;
            user.Type = userLogicModel.Type;
            user.RegisterTime = userLogicModel.RegisterTime;
            user.LastLoginTime = userLogicModel.LastLoginTime;
            user.Status = userLogicModel.Status;
        }
        public static void ConvertBugLogicModelToDbBug(this BugLogicModel bugLogicModel,Bug bug)
        {
            bug.BugId = bugLogicModel.BugId;
            bug.Title = bugLogicModel.Title;
            bug.Smmary = bugLogicModel.Smmary;
            bug.Description = bugLogicModel.Description;
            bug.Status = bugLogicModel.Status;
            bug.Type = bugLogicModel.Type;
            bug.Creator = bugLogicModel.Creator;
            bug.Createtime = bugLogicModel.Createtime;
        }
        public static void ConvertDeveloperLogicModelToDbDeveloper(this DeveloperLogicModel developerLogicModel, Developer developer)
        {
            developer.DeveloperId = developerLogicModel.DeveloperId;
            developer.FristName = developerLogicModel.FristName;
            developer.LastName = developerLogicModel.LastName;
            developer.Email = developerLogicModel.Email;
            developer.Status = developerLogicModel.Status;
            //developer.UserId = developerLogicModel.UserId;
        }
        public static void ConvertProjectLogicModelToDbProject(this ProjectLogicModel projectLogicModel, Project project)
        {
            project.ProjectId = projectLogicModel.ProjectId;
            project.ProjectName = projectLogicModel.ProjectName;
            project.Description = projectLogicModel.Description;
            project.MainContact = projectLogicModel.MainContact;
            project.ContactEmail = projectLogicModel.ContactEmail;
            project.StartTime = projectLogicModel.StartTime;
        }
        public static void ConvertCauseBugDeveloperLogicModelToDbCauseBugDeveloper(this CauseBugDeveloperLogicModel causeBugDeveloperLogicModel, CauseBugDeveloper causeBugDeveloper)
        {
            //causeBugDeveloper.BugId = causeBugDeveloperLogicModel.BugId;
            //causeBugDeveloper.DeveloperId = causeBugDeveloperLogicModel.DeveloperId;
            causeBugDeveloper.Id = causeBugDeveloperLogicModel.Id;
        }
    }
}

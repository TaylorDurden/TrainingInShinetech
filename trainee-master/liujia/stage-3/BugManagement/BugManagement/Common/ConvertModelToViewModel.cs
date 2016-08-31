using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugManagement.DAL.Model;
using BugManagement.Models;

namespace BugManagement
{
    public class ConvertModelToViewModel
    {
        public void ConvertBugToBugViewModel(Bug bug, BugViewModel bugViewModel)
        {
            bugViewModel.BugId = bug.BugId;
            bugViewModel.Title = bug.Title;
            bugViewModel.Smmary = bug.Smmary;
            bugViewModel.Description = bug.Description;
            bugViewModel.Status = bug.Status;
            bugViewModel.Type = bug.Type;
            bugViewModel.Creator = bug.Creator;
            bugViewModel.Createtime = bug.Createtime;
            bugViewModel.ProjectId = bug.ProjectId;
            bugViewModel.Project = bug.Project;
        }
        public void ConvertBugViewModelToBug(BugViewModel bugViewModel, Bug bug)
        {
            bug.BugId = bugViewModel.BugId;
            bug.Title = bugViewModel.Title;
            bug.Smmary = bugViewModel.Smmary;
            bug.Description = bugViewModel.Description;
            bug.Status = bugViewModel.Status;
            bug.Type = bugViewModel.Type;
            bug.Creator = bugViewModel.Creator;
            bug.Createtime = bugViewModel.Createtime;
            bug.ProjectId = bugViewModel.ProjectId;
            bug.Project = bugViewModel.Project;
        }
        public void ConvertUserToUserViewModel(User user, UserViewModel userViewModel)
        {
            userViewModel.UserId = user.UserId;
            userViewModel.FristName = user.FristName;
            userViewModel.LastName = user.LastName;
            userViewModel.Email = user.Email;
            userViewModel.Password = user.Password;
            userViewModel.Type = user.Type;
            userViewModel.RegisterTime = user.RegisterTime;
            userViewModel.LastLoginTime = user.LastLoginTime;
            userViewModel.Status = user.Status;
        }
        public void ConvertUserViewModelToUser(UserViewModel userViewModel, User user)
        {
            user.UserId = userViewModel.UserId;
            user.FristName = userViewModel.FristName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.Password = userViewModel.Password;
            user.Type = userViewModel.Type;
            user.RegisterTime = userViewModel.RegisterTime;
            user.LastLoginTime = userViewModel.LastLoginTime;
            user.Status = userViewModel.Status;
        }
        public void ConvertDeveloperToDeveloperViewModel(Developer developer, DeveloperViewModel developerViewModel)
        {
            developerViewModel.DeveloperId = developer.DeveloperId;
            developerViewModel.FristName = developer.FristName;
            developerViewModel.LastName = developer.LastName;
            developerViewModel.Email = developer.Email;
            developerViewModel.Status = developer.Status;
            developerViewModel.UserId = developer.UserId;
        }
        public void ConvertDeveloperViewModelToDeveloper(DeveloperViewModel developerViewModel, Developer developer)
        {
            developer.DeveloperId = developerViewModel.DeveloperId;
            developer.FristName = developerViewModel.FristName;
            developer.LastName = developerViewModel.LastName;
            developer.Email = developerViewModel.Email;
            developer.Status = developerViewModel.Status;
            developer.UserId = developerViewModel.UserId;
        }
        public void ConvertProjectToProjectViewModel(Project project, ProjectViewModel projectViewModel)
        {
            projectViewModel.ProjectId = project.ProjectId;
            projectViewModel.ProjectName = project.ProjectName;
            projectViewModel.Description = project.Description;
            projectViewModel.MainContact = project.MainContact;
            projectViewModel.ContactEmail = project.ContactEmail;
            projectViewModel.StartTime = project.StartTime;
        }
        public void ConvertProjectViewModelToProject(ProjectViewModel projectViewModel, Project project)
        {
            project.ProjectId = projectViewModel.ProjectId;
            project.ProjectName = projectViewModel.ProjectName;
            project.Description = projectViewModel.Description;
            project.MainContact = projectViewModel.MainContact;
            project.ContactEmail = projectViewModel.ContactEmail;
            project.StartTime = projectViewModel.StartTime;
        }
        public void ConvertRegisterViewModelToUser(RegisterViewModel registerViewModel, User user)
        {
            user.FristName = registerViewModel.FristName;
            user.LastName = registerViewModel.LastName;
            user.Email = registerViewModel.Email;
            user.Password = registerViewModel.Password;
        }
    }
}
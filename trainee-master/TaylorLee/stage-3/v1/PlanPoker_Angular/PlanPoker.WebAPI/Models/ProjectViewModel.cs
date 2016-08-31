using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using PlanPoker.Data.Models;

namespace PlanPoker.WebAPI.Models
{
    public class ProjectViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProjectGuid { get; set; }

        public string StartDate { get; set; }
    }

    public static class Convert
    {
        public static Project ConvertToProject(this ProjectViewModel projectViewModel)
        {
            return new Project()
            {
                Id = int.Parse(projectViewModel.Id??"0"),
                Name = projectViewModel.Name,
                ProjectGuid = Guid.Parse(projectViewModel.ProjectGuid),
                StartDate = DateTime.Parse(projectViewModel.StartDate)
            };
        }

        public static ProjectViewModel ConvertToProjectViewModel(this Project project)
        {
            return new ProjectViewModel()
            {
                Id = project.Id.ToString(),
                Name = project.Name,
                ProjectGuid = project.ProjectGuid.ToString(),
                StartDate = project.StartDate.ToString()
            };
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.Models
{
    public static class ProjectLogicConvert
    {
        public static Project CreateConvert(this ProjectLogicModel projectLogicModel)
        {
            return new Project
            {
                Id = projectLogicModel.Id,
                Name = projectLogicModel.Name,
                ProjectGuid = projectLogicModel.ProjectGuid

            };
        }

        public static Project EditConvert(this ProjectLogicModel projectLogicModel)
        {
            return new Project
            {
                Id = projectLogicModel.Id,
                Name = projectLogicModel.Name,
                ProjectGuid = projectLogicModel.ProjectGuid

            };
        }

        public static ProjectLogicModel GetConvert(this Project project)
        {
            return new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                ProjectGuid = project.ProjectGuid
            };
        }

        public static List<ProjectLogicModel> GetAllConvert(this List<Project> projects)
        {
            return projects.Select(project => new ProjectLogicModel
            {
                Id = project.Id, Name = project.Name, ProjectGuid = project.ProjectGuid
            }).ToList();
        }

        public static List<ProjectLogicModel> GetConvert(this List<Project> projects)
        {
            return projects.Select(project => new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                ProjectGuid = project.ProjectGuid
            }).ToList();
        }
    }
}

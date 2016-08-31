using System.Collections.Generic;
using System.Linq;
using PlanPoker.ILogic.Models;

namespace PlanPoker.WebAPI.Models
{
    public static class ProjectWebConvert
    {
        public static ProjectLogicModel CreateConvert(this ProjectWebModel projectWebModel)
        {
            return new ProjectLogicModel
            {
                Id = projectWebModel.Id,
                Name = projectWebModel.Name,
                ProjectGuid = projectWebModel.ProjectGuid
            };
        }

        public static ProjectLogicModel EditConvert(this ProjectWebModel projectWebModel)
        {
            return new ProjectLogicModel
            {
                Id = projectWebModel.Id,
                Name = projectWebModel.Name,
                ProjectGuid = projectWebModel.ProjectGuid
            };
        }

        public static List<ProjectWebModel> GetAllConvert(this List<ProjectLogicModel> projectLogicModels)
        {
            return projectLogicModels.Select(projectLogicModel => new ProjectWebModel
            {
                Id = projectLogicModel.Id,
                Name = projectLogicModel.Name,
                ProjectGuid = projectLogicModel.ProjectGuid
            }).ToList();
        }

        public static ProjectWebModel GetProjectByIdConvert(this ProjectLogicModel projectLogicModel)
        {
            return new ProjectWebModel
            {
                Id = projectLogicModel.Id,
                Name = projectLogicModel.Name,
                ProjectGuid = projectLogicModel.ProjectGuid
            };
        }

        public static string GetProjectUrlByIdConvert(this ProjectLogicModel projectLogicModel)
        {
            return projectLogicModel.ProjectGuid.ToString();
        }

        public static List<ProjectWebModel> GetProjectByNameConvert(this List<ProjectLogicModel> projectsLogic)
        {
            return projectsLogic.Select(projectLogic => new ProjectWebModel
            {
                Id = projectLogic.Id,
                Name = projectLogic.Name,
                ProjectGuid = projectLogic.ProjectGuid
            }).ToList();
        }

        public static ProjectWebModel GetProjectByGuidConvert(this ProjectLogicModel projectsLogic)
        {
            return new ProjectWebModel
            {
                Id = projectsLogic.Id,
                Name = projectsLogic.Name,
                ProjectGuid = projectsLogic.ProjectGuid
            };
        }

    }
}
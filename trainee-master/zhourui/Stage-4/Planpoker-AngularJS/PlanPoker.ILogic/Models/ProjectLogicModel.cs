using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.Models
{
    public class ProjectLogicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Message { get; set; }
        public bool Status { get; set; }
    }

    public static class ProjectConverter
    {
        public static Project ConvertToProjectEntity(this ProjectLogicModel model)
        {
            return new Project
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static ProjectLogicModel ConvertToProjectLogicModel(this Project project, string message, bool status)
        {
            return new ProjectLogicModel
            {
                Id = project.Id,
                Name = project.Name,
                Message = message,
                Status = status
            };
        }
    }
}
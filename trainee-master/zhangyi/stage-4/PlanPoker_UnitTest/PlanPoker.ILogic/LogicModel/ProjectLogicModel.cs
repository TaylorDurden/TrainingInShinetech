using System;
using PlanPoker.Data.Models;

namespace PlanPoker.ILogic.LogicModel
{
    public class ProjectLogicModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid ProjectGuid { get; set; }

        //public Project ChangeToProjectEntity()
        //{
        //    Project project = new Project
        //    {
        //        Id = Id,
        //        Name = Name,
        //        ProjectGuid = ProjectGuid
        //    };
        //    return project;
        //}

        //public void ChangeToProjectLogicModel(Project model)
        //{

        //    Id = model.Id;
        //    Name = model.Name;
        //    ProjectGuid = model.ProjectGuid;
        //}
    }
}
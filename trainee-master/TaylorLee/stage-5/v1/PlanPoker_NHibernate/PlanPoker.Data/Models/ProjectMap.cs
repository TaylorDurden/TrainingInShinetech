using FluentNHibernate.Mapping;

namespace PlanPoker.Data.Models
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Table("Project");
            Id(x => x.Id).Column("Id");
            Map(x => x.Name).Column("Name");
            Map(x => x.ProjectGuid).Column("ProjectGuid");
            Map(x => x.StartDate).Column("StartDate");
            LazyLoad();
            
        }
    }
}

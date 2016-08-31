using FluentNHibernate.Mapping;

namespace PlanPoker.Data.Models
{
    public class ProjectMap:ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.ProjectGuid);
        }
    }
}

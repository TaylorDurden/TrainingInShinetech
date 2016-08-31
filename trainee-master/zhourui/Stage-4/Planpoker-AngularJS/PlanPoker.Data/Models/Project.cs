using FluentNHibernate.Mapping;

namespace PlanPoker.Data.Models
{
    public class Project
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
    }

    public class ProjectMappings : ClassMap<Project>
    {
        public ProjectMappings()
        {
            Id(x => x.Id);
            Map(x => x.Name).Length(10).Not.Nullable();
        }
    }
}
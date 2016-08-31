using FluentNHibernate.Mapping;
using PlanPoker.Data.Models;

namespace RepositoryNhibernate.Mapping
{
    public class ProjectMapping : ClassMap<Project>
    {
        public ProjectMapping()
        {
            Table("Project");
            Id(x => x.Id);
            Map(m => m.Name);
            Map(m => m.ProjectGuid);
        }
    }
}

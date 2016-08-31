using FluentNHibernate.Mapping;

namespace BugManagement.Data.Models.Mappings
{
    public class ProjectMap:ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.ProjectId);

            Map(x => x.ProjectName);

            Map(x => x.ContactEmail);

            Map(x => x.Description);

            Map(x => x.MainContact);

            Map(x => x.StartTime).Nullable();

            HasMany(u => u.Bugs).KeyColumn("ProjectId").Cascade.All();

            Table("Project");
        }
    }
}

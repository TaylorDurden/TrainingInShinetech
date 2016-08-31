using FluentNHibernate.Mapping;

namespace BugManagement.Data.Models.Mappings
{
    public class BugMap:ClassMap<Bug>
    {
        public BugMap()
        {
            Id(x => x.BugId);
            Map(x => x.Title);
            Map(x => x.Smmary);
            Map(x => x.Description);
            Map(x => x.Status);
            Map(x => x.Type);
            Map(x => x.Creator);
            Map(x => x.Createtime).Nullable();

            References(x => x.Project, "ProjectId");

            HasMany(u => u.CauseBugDevelopers).KeyColumn("BugId").Cascade.All();

            HasMany(u => u.Documents).KeyColumn("BugId").Cascade.All();

            Table("Bug");
        }
    }
}

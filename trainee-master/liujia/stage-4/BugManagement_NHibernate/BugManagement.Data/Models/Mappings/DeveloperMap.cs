using FluentNHibernate.Mapping;

namespace BugManagement.Data.Models.Mappings
{
    public class DeveloperMap:ClassMap<Developer>
    {
        public DeveloperMap()
        {
            Id(x => x.DeveloperId);
            Map(x => x.FristName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.Status);
            
            //HasMany(u => u.CauseBugDevelopers).KeyColumn("DeveloperId").Cascade.All().Inverse();
            HasMany(u => u.CauseBugDevelopers).KeyColumn("DeveloperId").Cascade.All();

            References(x => x.User, "UserId");

            Table("Developer");
        }
    }
}

using FluentNHibernate.Mapping;

namespace BugManagement.Data.Models.Mappings
{
    public class CauseBugDeveloperMap:ClassMap<CauseBugDeveloper>
    {
        public CauseBugDeveloperMap()
        {
            Id(x => x.Id);
            
            //HasOne(u => u.Bug).ForeignKey("BugId").Cascade.All().Fetch.Select();
            //HasOne(u => u.Developer).ForeignKey("DeveloperId").Cascade.All().Fetch.Select();

            References(x => x.Bug, "BugId");
            References(x => x.Developer, "DeveloperId");

            Table("CauseBugDeveloper");
        }
    }
}

using FluentNHibernate.Mapping;

namespace BugManagement.Data.Models.Mappings
{
    public class BugTypeMap:ClassMap<BugType>
    {
        public BugTypeMap()
        {
            Id(x => x.BugTypeId);
            Map(x => x.Name);
            Map(x => x.Status);
            Table("BugType");
        }
    }
}

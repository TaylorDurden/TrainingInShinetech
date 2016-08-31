using FluentNHibernate.Mapping;

namespace BugManagement.Data.Models.Mappings
{
    public class DocumentMap:ClassMap<Document>
    {
        public DocumentMap()
        {
            Id(x => x.DocumentId);
            Map(x => x.Path);
            
            References(x => x.Bug, "BugId");

            Table("Document");
        }
    }
}

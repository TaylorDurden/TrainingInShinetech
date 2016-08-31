namespace BugManagement.Data.Models.Mappings
{
    public class Document
    {
        public virtual int DocumentId { get; set; }
        public virtual string Path { get; set; }

        public virtual Bug Bug { get; set; }
    }
}

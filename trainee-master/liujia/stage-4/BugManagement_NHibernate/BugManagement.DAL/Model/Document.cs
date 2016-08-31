namespace BugManagement.DAL.Model
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int BugId { get; set; }
        public string Path { get; set; }
        
        public virtual Bug Bug { get; set; }
    }
}

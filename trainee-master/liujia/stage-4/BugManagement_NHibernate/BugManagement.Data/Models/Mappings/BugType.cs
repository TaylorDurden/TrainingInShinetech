namespace BugManagement.Data.Models.Mappings
{
    public class BugType
    {
        public virtual int BugTypeId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Status { get; set; }
    }
}

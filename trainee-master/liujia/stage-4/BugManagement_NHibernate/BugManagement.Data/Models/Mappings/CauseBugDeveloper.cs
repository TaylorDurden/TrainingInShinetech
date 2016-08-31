namespace BugManagement.Data.Models.Mappings
{
    public class CauseBugDeveloper
    {
        public virtual int Id { get; set; }
        public virtual Bug Bug { get; set; }
        public virtual Developer Developer { get; set; }
    }
}

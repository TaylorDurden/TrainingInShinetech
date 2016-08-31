namespace BugManagement.DAL.Model
{
    public class CauseBugDeveloper
    {
        public int Id { get; set; }
        public int BugId { get; set; }
        public int DeveloperId { get; set; }
        public virtual Bug Bug { get; set; }
        public virtual Developer Developer { get; set; }
    }
}

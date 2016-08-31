using System.ComponentModel.DataAnnotations;

namespace BugManagement.Data.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        public int BugId { get; set; }
        public string Path { get; set; }
    }
}

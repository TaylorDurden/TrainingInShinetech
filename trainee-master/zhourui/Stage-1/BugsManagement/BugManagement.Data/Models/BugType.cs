using System.ComponentModel.DataAnnotations;

namespace BugManagement.Data.Models
{
    public class BugType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

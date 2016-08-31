using BugManagement.DAL.Model;

namespace BugManagement.Logic.Models
{
    public class DeveloperLogicModel
    {
        public int DeveloperId { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}

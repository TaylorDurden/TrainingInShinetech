using System;
namespace BugManagement.DAL.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Type { get; set; }

        public DateTime? RegisterTime { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public string Status { get; set; }
    }
}

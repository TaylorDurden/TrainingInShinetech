using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.SqlServer.Server;

namespace BugManagement.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public UserStatus Status { get; set; }

        public enum UserType
        {
            Admin = 0,
            QA = 1,
            Developer = 2,
            Anonymous = 3
        };

        public enum UserStatus
        {
            New = 0,
            Checked = 1,
            Deleted = 2
        };
    }
}

using BugManagement.DAL.Model;
using BugManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugManagement.Models
{
    public class UserListViewModel
    {
        public string whereCondition { get; set; }
        public IEnumerable<UserViewModel> modelList { get; set; }
        public UserViewModel model{ get; set; }

        public PagingInfo<UserViewModel> pagingInfo { get; set; }    
}
    public class UserViewModel
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
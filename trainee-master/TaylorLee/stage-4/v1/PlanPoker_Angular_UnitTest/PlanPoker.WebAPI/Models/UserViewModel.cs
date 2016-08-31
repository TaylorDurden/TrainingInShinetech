using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.Models;

namespace PlanPoker.WebAPI.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
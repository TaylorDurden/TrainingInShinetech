﻿using System.ComponentModel.DataAnnotations;

namespace PlanPoker.Data.Models
{
    public class User
    {
        //[Key]
        public virtual int UserId { get; set; }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Image { get; set; }
    }
}

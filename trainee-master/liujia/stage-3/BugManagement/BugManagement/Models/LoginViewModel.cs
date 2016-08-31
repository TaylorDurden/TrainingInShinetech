using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugManagement.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is not null!")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email format is not correct! ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not null! ")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
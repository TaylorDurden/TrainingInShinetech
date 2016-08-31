using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugManagement.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Frist Name is not null!")]
        [Display(Name = "Frist Name")]
        public string FristName { get; set; }

        [Required(ErrorMessage = "Last Name is not null!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is not null!")]
        [EmailAddress(ErrorMessage = "Email Address format is not correct! ")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not null! ")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repart Password is not null! ")]
        [DataType(DataType.Password)]
        [Display(Name = "Repart  password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and repart  password do not match.")]
        public string RepartPassword { get; set; }
                
        [Display(Name = "ReadAccept")]
        public bool ReadAccept { get; set; }
    }    
}
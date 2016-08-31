using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Remoting.Channels;

namespace PlanPoker.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

        [InverseProperty("Creator")]
        public virtual ICollection<Project> OwnedProjects { get; set; }
        [InverseProperty("RelatedUsers")]
        public virtual ICollection<Project> RelatedProjects { get; set; }
    }
}
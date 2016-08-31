using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanPoker.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [InverseProperty("RelatedUsers")]
        public virtual ICollection<User> InvitedUsers { get; set; }
    }
}
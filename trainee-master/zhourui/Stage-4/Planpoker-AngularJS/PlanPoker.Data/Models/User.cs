using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;

namespace PlanPoker.Data.Models
{
    public class User
    {
        public virtual int UserId { get; set; }

        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Image { get; set; }
    }

    public class UserMappings: ClassMap<User>
    {
        public UserMappings()
        {
            Id(x => x.UserId);
            Map(x => x.UserName).Length(10).Not.Nullable();
            Map(x => x.Password).Length(8).Not.Nullable();
            Map(x => x.Email).Length(20).Not.Nullable();
            Map(x => x.Image).Length(65535);
            Table("[User]");
        }
    }
}
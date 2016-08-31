using System;
using FluentNHibernate.Mapping;

namespace PlanPoker.Data.Models
{
    public class UserMap : ClassMap<User>
    {
        public UserMap ()
        {
            Table("[User]");
            Id(x => x.UserId).GeneratedBy.Native().Column("UserId");
            Map(x => x.UserName).Column("UserName");
            Map(x => x.Password).Column("Password");
            Map(x => x.Email).Column("Email");
            Map(x => x.Image).Column("Image").Length(8000);
            LazyLoad();
        }
    }
}

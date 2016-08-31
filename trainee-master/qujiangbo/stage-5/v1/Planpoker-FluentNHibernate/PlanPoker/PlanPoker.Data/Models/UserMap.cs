using FluentNHibernate.Mapping;

namespace PlanPoker.Data.Models
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.UserId).GeneratedBy.Identity();
            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.Email);
            Map(x => x.Image).Length(8000);
        }
    }
}

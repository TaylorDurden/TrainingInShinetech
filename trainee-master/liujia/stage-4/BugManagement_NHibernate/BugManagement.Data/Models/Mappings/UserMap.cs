using FluentNHibernate.Mapping;

namespace BugManagement.Data.Models.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.UserId).GeneratedBy.Native();

            Map(x => x.FristName);

            Map(x => x.LastName);

            Map(x => x.Email);

            Map(x => x.Password);

            Map(x => x.Type);

            Map(x => x.RegisterTime).Nullable();

            Map(x => x.LastLoginTime).Nullable();

            Map(x => x.Status);

            HasMany(u => u.Developers).Cascade.All();

            Table("[User]");
        }
    }
}

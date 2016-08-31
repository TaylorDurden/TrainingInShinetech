using FluentNHibernate.Mapping;
using PlanPoker.Data.Models;

namespace RepositoryNhibernate.Mapping
{
    public class PlanPokerUserMapping : ClassMap<PlanPokerUser>
    {
        public PlanPokerUserMapping()
        {
            Table("PlanPokerUser");
            Id(x => x.UserId);
            Map(m => m.Password);
            Map(m => m.Email);
            Map(m => m.UserName);
            Map(m => m.Image).Length(8000);
        }
    }
}

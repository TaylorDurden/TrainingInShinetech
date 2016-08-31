using FluentNHibernate.Domain.Models;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Domain.Mapping
{
    public class CustomerMapping : ClassMap<Customer>
    {
        public CustomerMapping()
        {
            Table("Customer");
            Id(x => x.CustomerId).GeneratedBy.Increment();
            Map(m => m.CustomerAddress).Length(50).Nullable();
            Map(m => m.CustomerName).Length(32).Nullable();
            Map(m => m.Version);
        }
    }
}
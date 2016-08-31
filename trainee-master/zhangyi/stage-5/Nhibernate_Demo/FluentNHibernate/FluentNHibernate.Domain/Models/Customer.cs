namespace FluentNHibernate.Domain.Models
{
    public class Customer
    {
        public virtual int CustomerId { set; get; }
          public virtual int Version { set; get; }
          public virtual string CustomerName { set; get; }
          public virtual string CustomerAddress { set; get; }
    }
}
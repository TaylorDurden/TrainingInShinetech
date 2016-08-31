using NHibernate;

namespace RepositoryNhibernate.Dal
{
    public interface IFluentNHibernateHelper
    {
        ISession GetSession();
    }
}
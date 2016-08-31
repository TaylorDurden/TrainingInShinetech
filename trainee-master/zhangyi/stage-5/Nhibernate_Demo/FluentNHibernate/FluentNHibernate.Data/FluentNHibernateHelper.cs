using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Domain.Models;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace FluentNHibernate.Data
{
    public class FluentNHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISession _session;
        private static readonly object ObjLock = new object();
        

        private static ISessionFactory GetSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2000
                  .ConnectionString(
                  @"Data Source=.; Initial Catalog=PlanPokerDB; Integrated Security=SSPI")
                              
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Customer>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(false, false))
                .BuildSessionFactory();
            return _sessionFactory;
        }

        public static ISession GetSession()
        {
            GetSessionFactory();
            if (_session != null) return _session;
            lock (ObjLock)
            {
                _session = _sessionFactory.OpenSession();
            }
            return _session;
        }
    }
}
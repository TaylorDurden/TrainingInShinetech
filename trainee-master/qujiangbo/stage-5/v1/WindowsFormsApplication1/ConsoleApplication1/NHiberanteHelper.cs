using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace ConsoleApplication1
{
    public sealed class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)

                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                  .ConnectionString(
                  @"Data Source=.;Initial Catalog=TestDB;Integrated Security=SSPI;")
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<MakeMap>()
                              .AddFromAssemblyOf<ModelMap>()
                              .AddFromAssemblyOf<CarMap>()
                              .AddFromAssemblyOf<UserMapping>()
                              .AddFromAssemblyOf<UserDetailMapping>())
                .ExposeConfiguration(
                                cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}

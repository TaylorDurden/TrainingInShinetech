using BugManagement.Data.Models.Mappings;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace BugManagement.Dao
{
    public class NHibernateConfigurator
    {
        public static void Configure(IWindsorContainer container)
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("BugManagementConnectionString")))
                //        .Database(MsSqlConfiguration.MsSql2012
                //            .ConnectionString(
                //                @"Server=.;initial catalog=BugManagement;
                //user=sa;password=1qaz2wsx;")
                //            .ShowSql()
                //        )
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<UserMap>())
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();

            container.Register(Component.For<ISessionFactory>().Instance(sessionFactory).LifestyleSingleton());
        }
    }
}

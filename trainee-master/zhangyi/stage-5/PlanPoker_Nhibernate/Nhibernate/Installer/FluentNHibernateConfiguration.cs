using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using RepositoryNhibernate.Mapping;

namespace RepositoryNhibernate.Installer
{
    public class FluentNHibernateConfiguration
    {

        public static void Configure(IWindsorContainer container)
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2000
                    .ConnectionString(
                        @"Data Source=.; Initial Catalog=PlanPokerDB; Integrated Security=SSPI")
                )
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<ProjectMapping>()
                        .AddFromAssemblyOf<PlanPokerUserMapping>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                    .Create(false, false));
            var sessionFactory = configuration.BuildSessionFactory();
            container.Register(Component.For<ISessionFactory>().Instance(sessionFactory).LifestyleSingleton());

        }
    }
}
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using PlanPoker.Data.Models;

namespace PlanPoker.Repository.Installer
{
    public class NHibernateConfigurator
    {
        public static void Configure(IWindsorContainer container)
        {
            var configuration = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(c => c.FromConnectionStringWithKey("PlanPokerConnectionString")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProjectMappings>())
                .BuildConfiguration();
            var sessionFactory = configuration.BuildSessionFactory();
            container.Register(Component.For<ISessionFactory>().Instance(sessionFactory).LifestyleSingleton());
        }
    }
}

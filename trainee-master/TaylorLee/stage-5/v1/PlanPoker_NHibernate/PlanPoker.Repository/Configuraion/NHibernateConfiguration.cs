using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PlanPoker.Data.Models;

namespace PlanPoker.Repository.Configuraion
{
    public class NHibernateConfiguration
    {
        public static void Configure(IWindsorContainer container)
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration
                    .MsSql2008
                    .ConnectionString("Data Source=.; Initial Catalog=PlanPokerDB; Uid=PlanPokerDB;Password=PlanPokerDB"))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserMap>()
                    .AddFromAssemblyOf<ProjectMap>())
                    .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))
                .BuildConfiguration().SetDefaultAssembly("dbo");
            var sessionFactory = configuration.BuildSessionFactory();
            container.Register(Component.For<ISessionFactory>().Instance(sessionFactory).LifestyleSingleton());
        }
    }
}

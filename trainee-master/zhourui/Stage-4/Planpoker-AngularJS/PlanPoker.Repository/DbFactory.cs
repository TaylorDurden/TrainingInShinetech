using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PlanPoker.Data.Models;

namespace PlanPoker.Repository
{
    public class DbFactory
    {
        //public static ISessionFactory GetCurrentSession()
        //{
        //    var sessionFactory = new Configuration().Configure(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/Config/hibernate.cfg.xml").BuildSessionFactory();

        //    return sessionFactory;
        //}

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012
            .ConnectionString(c => c.FromConnectionStringWithKey("PlanPokerConnectionString")))
            .Mappings(m => m.FluentMappings
            .AddFromAssemblyOf<UserMappings>()
            .AddFromAssemblyOf<ProjectMappings>())
            .ExposeConfiguration(CreateSchema)
            .BuildSessionFactory();
        }

        //public static ISessionFactory CreateSessionFactoryForTest()
        //{
        //    return Fluently.Configure()
        //    .Database(MsSqlConfiguration
        //    .MsSql2008
        //    .ConnectionString("Data Source=.; Initial Catalog=PlanPokerDBTest; Uid=sa;Password=1"))
        //    .Mappings(m => m.FluentMappings
        //    .AddFromAssemblyOf<UserMappings>()
        //    .AddFromAssemblyOf<ProjectMappings>())
        //    .ExposeConfiguration(CreateSchema)
        //    .BuildSessionFactory();
        //}

        private static void CreateSchema(Configuration cfg)
        {
            var schemaExport = new SchemaExport(cfg);
            schemaExport.Create(false, false);
        }
    }
}

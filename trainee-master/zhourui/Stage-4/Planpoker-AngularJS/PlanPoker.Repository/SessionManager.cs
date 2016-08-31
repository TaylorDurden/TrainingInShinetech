using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PlanPoker.Data.Models;

namespace PlanPoker.Repository
{
    public class SessionManager
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionManager()
        {
            _sessionFactory = GetSessionFactory();
        }
        
        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        private static ISessionFactory GetSessionFactory()
        {
            return Fluently.Configure()
            .Database(MsSqlConfiguration
            .MsSql2008
            .ConnectionString(c => c.FromConnectionStringWithKey("PlanPokerConnectionString")))
            .Mappings(m => m.FluentMappings
            .AddFromAssemblyOf<UserMappings>()
            .AddFromAssemblyOf<ProjectMappings>())
            .ExposeConfiguration(CreateSchema)
            .BuildSessionFactory();
        }

        private static void CreateSchema(Configuration cfg)
        {
            var schemaExport = new SchemaExport(cfg);
            schemaExport.Create(false, false);
        }
    }
}

using System.Data.Entity;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PlanPoker.Data.Models;
using PlanPoker.Data.SessionHelper;

namespace PlanPoker.Data
{
    public class DataBaseFactory : IDbFactory
    {
        private readonly IWindsorContainer _container;

        public DataBaseFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public DbContext GetContext()
        {
            return _container.Resolve<PlanPokerContext>();
        }

        public ISession GetISessionContext()
        {
            return _container.Resolve<ISessionProvider>().GetCurrentSession();
        }
    }
}

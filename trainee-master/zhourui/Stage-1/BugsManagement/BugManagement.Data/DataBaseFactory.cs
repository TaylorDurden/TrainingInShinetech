﻿using System.Data.Entity;
using Castle.Windsor;

namespace BugManagement.Data
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
            return _container.Resolve<BugManagementContext>();
        }
    }
}

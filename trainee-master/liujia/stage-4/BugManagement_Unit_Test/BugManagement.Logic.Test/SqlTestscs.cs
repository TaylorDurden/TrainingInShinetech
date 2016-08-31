using System;
using BugManagement.Data.Models.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NUnit.Framework;

namespace BugManagement.Logic.Test
{
    class SqlTestscs
    {
        [Test]
        public void Test1()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("BugManagementConnectionString")))
                //        .Database(MsSqlConfiguration.MsSql2012
                //            .ConnectionString(
                //                @"Server=.;initial catalog=BugManagement;
                //user=sa;password=1qaz2wsx;")
                //            .ShowSql()
                //        )
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<UserMap>()
                        .AddFromAssemblyOf<BugTypeMap>()
                        //.AddFromAssemblyOf<BugMap>()
                        //.AddFromAssemblyOf<CauseBugDeveloperMap>()
                        .AddFromAssemblyOf<DeveloperMap>()
                        //.AddFromAssemblyOf<DocumentMap>()
                        .AddFromAssemblyOf<ProjectMap>())
//                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();

            var user = new User
            {
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };

            var dev = new Developer
            {
                Email = "test",
                FristName = "fristname",
                LastName = "lastname",
                Status = "status",
                User = user
            };

            
            using (var session = sessionFactory.OpenSession())
            {
                using (var traction = session.BeginTransaction())
                {
                    session.Save(user);
                    session.Save(dev);
                    traction.Commit();
                }
            }

            using (var session = sessionFactory.OpenSession())
            {
                using (var traction = session.BeginTransaction())
                {
                    session.Delete(session.Get<User>(user.UserId));
                    session.Flush();
                    traction.Commit();
                }
            }
        }
    }
}

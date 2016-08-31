using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.Repository.Installer;

namespace PlanPoker.Logic.Tests.NewFolder1
{
    public class TestBase
    {
        protected IWindsorContainer Container;
        protected IDisposable Scope;

        [SetUp]
        public void SetUp()
        {
            if (File.Exists("test.sdf"))
            {
                File.Delete("test.sdf");
            }
            SqlCeEngine engine = new SqlCeEngine("Data Source=test.sdf");
            engine.CreateDatabase();

            var configuariton = Fluently.Configure().Database(MsSqlCeConfiguration.MsSqlCe40
          .ConnectionString("Data Source=|DataDirectory|test.sdf"))
          .Mappings(m => m.FluentMappings
          .AddFromAssemblyOf<ProjectMappings>())
          .BuildConfiguration();

            var se = new SchemaExport(configuariton);
            se.Create(useStdOut: true, execute: true);

            var sessionFactory = configuariton.BuildSessionFactory();

            Container = new WindsorContainer();
            Container.Kernel.ComponentModelBuilder.AddContributor(new LifeStyleConstruction());
            Container.Install(FromAssembly.Containing<Logic.Installer.LogicInstaller>(),
                FromAssembly.Containing<RepositoryInstaller>()
            );
            Container.Register(Component.For<ISession>()
                .UsingFactoryMethod(k => sessionFactory.OpenSession())
                .LifestylePerWebRequest(),
                Component.For<IWindsorContainer>().Instance(Container));

            Scope = Container.BeginScope();
        }

        [Test]
        public void TestXX()
        {
            var userLogic = Container.Resolve<IUserLogic>();
            var user = new UserLogicModel
            {
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };

            userLogic.Create(user);
        }

        //[Test]
        //public void TestYY()
        //{
        //    var userLogic = Container.Resolve<IUserLogic>();

        //    Assert.Throws<Email>(() => userLogic.Create());
        //}

        [TearDown]
        public void TearDown()
        {
            Scope.Dispose();
        }
    }

    public class LifeStyleConstruction : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if (model.LifestyleType == LifestyleType.PerWebRequest)
            {
                model.LifestyleType = LifestyleType.Scoped;
            }
        }
    }
}

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using PlanPoker.Dao;
using PlanPoker.Data;
using PlanPoker.Data.SessionHelper;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Repository.Installer
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<UnitOfWorkFactory>().LifestylePerWebRequest(),
                Component.For<IUnitOfWork>().ImplementedBy<EntityFrameworkUnitOfWork>().LifestylePerWebRequest(),
                Component.For<IDbFactory>().ImplementedBy<DataBaseFactory>().LifestyleSingleton(),
                Component.For<ISessionProvider>().ImplementedBy<SessionProvider>().LifestyleSingleton(),
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession()),
                Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestylePerWebRequest(),
                Component.For<IProjectRepository>().ImplementedBy<ProjectRepository>().LifestylePerWebRequest()
                );
        }
    }
}

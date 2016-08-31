using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using PlanPoker.IRepository;
using RepositoryNhibernate.Dal;
using RepositoryNhibernate.UnitOfWork;

namespace RepositoryNhibernate.Installer
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<UnitOfWorkFactory>().LifestylePerWebRequest(),
                Component.For<IUnitOfWork>().ImplementedBy<NHibernateUnitOfWork>().LifestylePerWebRequest(),
                Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleSingleton(),
                Component.For<IProjectRepository>().ImplementedBy<ProjectRepository>().LifestyleSingleton(),
                Component.For<IFluentNHibernateHelper>().ImplementedBy<FluentNHibernateHelper>().LifestyleSingleton(),
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                );
        }
    }
}

using BugManagement.Data;
using BugManagement.IRepository;
using BugManagement.Repository.UnitOfWork;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BugManagement.Repository.Installer
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<UnitOfWorkFactry>().LifestylePerWebRequest(),
                Component.For<IUnitOfWork>().ImplementedBy<EntityFrameworkUnitOfWork>().LifestylePerWebRequest(),
                Component.For<IDbFactory>().ImplementedBy<DataBaseFactory>().LifestyleSingleton(),
                Component.For<BugManagementContext>().LifestylePerWebRequest(),
                Component.For<IBugRepository>().ImplementedBy<BugRepository>().LifestylePerWebRequest(),
                Component.For<IBugTypeRepository>().ImplementedBy<BugTypeRepository>().LifestylePerWebRequest(),
                Component.For<IDeveloperRepository>().ImplementedBy<DeveloperRepository>().LifestylePerWebRequest(),
                Component.For<IProjectRepository>().ImplementedBy<ProjectRepository>().LifestylePerWebRequest(),
                Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestylePerWebRequest()
                );
        }
    }
}
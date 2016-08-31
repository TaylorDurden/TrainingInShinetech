using BugManagement.DAL.IRepository;
using BugManagement.DAL.Repository;
using BugManagement.DAL.UnitOfWork;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BugManagement.DAL
{
    public class RepositoryInstaller: IWindsorInstaller
    {
        public  void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<UnitOfWorkFactory>().LifestylePerWebRequest(),
                Component.For<IUnitOfWork>().ImplementedBy<EntityFrameworkUnitOfWork>().LifestylePerWebRequest(),
                Component.For<IDbFactory>().ImplementedBy<DataBaseFactory>().LifestyleSingleton(),
                Component.For<BugManagementContext>().LifestylePerWebRequest(),
                Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleSingleton(),
                Component.For<IBugRepository>().ImplementedBy<BugRepository>().LifestyleSingleton(),
                Component.For<IBugTypeRepository>().ImplementedBy<BugTypeRepository>().LifestyleSingleton(),
                Component.For<ICauseBugDeveloperRepository>().ImplementedBy<CauseBugDeveloperRepository>().LifestyleSingleton(),
                Component.For<IDeveloperRepository>().ImplementedBy<DeveloperRepository>().LifestyleSingleton(),
                Component.For<IProjectRepository>().ImplementedBy<ProjectRepository>().LifestyleSingleton(),
                Component.For<IDocumentRepository>().ImplementedBy<DocumentRepository>().LifestyleSingleton()
                );
        }
    }
}

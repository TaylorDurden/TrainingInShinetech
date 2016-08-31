using BugManagement.Dao.Dao;
using BugManagement.Dao.IDao;
using BugManagement.Dao.UnitOfWork;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;

namespace BugManagement.Dao
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<UnitOfWorkFactory>().LifestylePerWebRequest(),
                Component.For<IUnitOfWork>().ImplementedBy<NhibernateUnitOfWork>().LifestylePerWebRequest(),
                Component.For<IUserDao>().ImplementedBy<UserDao>().LifestylePerWebRequest(),
                Component.For<IBugDao>().ImplementedBy<BugDao>().LifestylePerWebRequest(),
                Component.For<IBugTypeDao>().ImplementedBy<BugTypeDao>().LifestylePerWebRequest(),
                Component.For<ICauseBugDeveloperDao>().ImplementedBy<CauseBugDeveloperDao>().LifestylePerWebRequest(),
                Component.For<IDeveloperDao>().ImplementedBy<DeveloperDao>().LifestylePerWebRequest(),
                Component.For<IProjectDao>().ImplementedBy<ProjectDao>().LifestylePerWebRequest(),
                Component.For<IDocumentDao>().ImplementedBy<DocumentDao>().LifestylePerWebRequest(),
                Component.For<ISessionProvider>().ImplementedBy<SessionProvider>().LifestylePerWebRequest(),
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession()).LifestylePerWebRequest()

                );
        }
    }
}
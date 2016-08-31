using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PlanPoker.IRepository;

namespace PlanPoker.Dao.Installer
{
    public class DaoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(
            //    Component.For<IUserRepository>().ImplementedBy<UserDao>().LifestylePerWebRequest(),
            //    Component.For<IProjectRepository>().ImplementedBy<ProjectDao>().LifestylePerWebRequest()
            //    );
        }
    }
}

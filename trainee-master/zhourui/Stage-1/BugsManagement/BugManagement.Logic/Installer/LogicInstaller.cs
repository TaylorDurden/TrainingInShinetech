using BugManagement.ILogic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BugManagement.Logic.Installer
{
    public class LogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IBugLogic>().ImplementedBy<BugLogic>().LifestylePerWebRequest(),
                Component.For<IBugTypeLogic>().ImplementedBy<BugTypeLogic>().LifestylePerWebRequest(),
                Component.For<IDeveloperLogic>().ImplementedBy<DeveloperLogic>().LifestylePerWebRequest(),
                Component.For<IProjectLogic>().ImplementedBy<ProjectLogic>().LifestylePerWebRequest(),
                Component.For<IUserLogic>().ImplementedBy<UserLogic>().LifestylePerWebRequest()
                );
        }
    }
}
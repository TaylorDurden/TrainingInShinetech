using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PlanPoker.ILogic;

namespace PlanPoker.Logic.Installer
{
    public class LogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUserLogic>().ImplementedBy<UserLogic>().LifestylePerWebRequest(),
                Component.For<IProjectLogic>().ImplementedBy<ProjectLogic>().LifestylePerWebRequest()
                );
        }
    }
}

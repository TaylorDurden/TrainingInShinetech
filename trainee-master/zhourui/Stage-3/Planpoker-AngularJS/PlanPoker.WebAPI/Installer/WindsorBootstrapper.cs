using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using PlanPoker.Logic.Installer;
using PlanPoker.Repository.Installer;

namespace PlanPoker.WebAPI.Installer
{
    public static class WindsorBootstrapper
    {
        public static IWindsorContainer Container { get; private set; }

        public static void Initialize()
        {
            Container = new WindsorContainer();
            Container.Install(FromAssembly.This(),
                FromAssembly.Containing<RepositoryInstaller>(),
                FromAssembly.Containing<LogicInstaller>()
                );

            Container.Register(Component.For<IWindsorContainer>().Instance(Container).LifestyleSingleton());
        }
    }
}
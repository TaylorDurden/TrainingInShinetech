using BugManagement.Dao;
using BugManagement.Logic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace BugManagemnet.WebAPI.Installer
{
    public static class WindsorBootstrapper
    {
        public static void Initialize()
        {
            Container = new WindsorContainer();
            
            Container.Install(FromAssembly.This(),
                FromAssembly.Containing<RepositoryInstaller>(),
                FromAssembly.Containing<LogicInstaller>()
                );

            Container.Register(Component.For<IWindsorContainer>().Instance(Container).LifestyleSingleton());
            NHibernateConfigurator.Configure(Container);
        }

        public static IWindsorContainer Container { get; private set; }
    }
}
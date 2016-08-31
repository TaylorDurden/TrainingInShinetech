using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using PlanPoker.Logic.Installer;
using RepositoryNhibernate.Installer;

namespace PlanPoker.WebAPI.Installer
{
    public static class WindsorBootstrapper
    {
        private static IWindsorContainer _container;

        public static void Initialize()
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<IWindsorContainer>().Instance(_container).LifestyleSingleton());
            _container.Install(FromAssembly.This(),
                FromAssembly.Containing<LogicInstaller>(),
                FromAssembly.Containing<RepositoryInstaller>()
                );

            FluentNHibernateConfiguration.Configure(Container);
        }

        public static IWindsorContainer Container => _container;
    }
}
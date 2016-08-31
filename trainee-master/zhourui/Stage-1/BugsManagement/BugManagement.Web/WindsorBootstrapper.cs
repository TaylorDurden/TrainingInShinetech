using System.Web.Mvc;
using BugManagement.Logic.Installer;
using BugManagement.Repository.Installer;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace BugManagement.Web
{
    public static class WindsorBootstrapper
    {
        private static IWindsorContainer _container;

        public static void Initialize()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This(),
                FromAssembly.Containing<RepositoryInstaller>(),
                FromAssembly.Containing<LogicInstaller>()
                );

            _container.Register(Component.For<IWindsorContainer>().Instance(_container).LifestyleSingleton());
            var controllerFactory = _container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static IWindsorContainer Container
        {
            get
            {
                return _container;
            }
        }
    }
}
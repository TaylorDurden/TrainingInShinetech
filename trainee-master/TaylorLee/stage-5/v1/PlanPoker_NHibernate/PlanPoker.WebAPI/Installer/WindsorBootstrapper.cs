using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using PlanPoker.Dao.Installer;
using PlanPoker.Logic.Installer;
using PlanPoker.Repository.Configuraion;
using PlanPoker.Repository.Installer;

namespace PlanPoker.WebAPI.Installer
{
    public static class WindsorBootstrapper
    {
        private static IWindsorContainer _container;

        public static void Initialize()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This(),
                FromAssembly.Containing<DaoInstaller>(),
                FromAssembly.Containing<RepositoryInstaller>(),
                FromAssembly.Containing<LogicInstaller>()
                );

            _container.Register(Component.For<IWindsorContainer>().Instance(_container).LifestyleSingleton());
            NHibernateConfiguration.Configure(_container);
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
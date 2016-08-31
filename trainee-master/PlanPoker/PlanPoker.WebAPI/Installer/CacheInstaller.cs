using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PlanPoker.Common;

namespace PlanPoker.WebAPI.Installer
{
    public class CacheInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICacheManger>().ImplementedBy<MemoryCacheManager>().LifestyleSingleton()
                );
        }
    }
}
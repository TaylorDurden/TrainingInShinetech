using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using BugManagement.Logic.ILogic;
using BugManagement.Logic.Logic;

namespace BugManagemnet.WebAPI.Installer
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
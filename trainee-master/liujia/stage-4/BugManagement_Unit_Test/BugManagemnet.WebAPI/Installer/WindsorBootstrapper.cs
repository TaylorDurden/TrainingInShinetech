using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugManagement.Logic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using BugManagement.DAL;

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
        }

        public static IWindsorContainer Container { get; private set; }
    }
}
﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using PlanPoker.IRepository;
using PlanPoker.Repository.SessionHelper;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Repository.Installer
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkFactory>().ImplementedBy<UnitOfWorkFactory>().LifestylePerWebRequest(),
                Component.For<IUnitOfWork>().ImplementedBy<NHibernateUnitOfWork>().LifestylePerWebRequest(),
                //Component.For<IDbFactory>().ImplementedBy<DataBaseFactory>().LifestyleSingleton(),
                //Component.For<PlanPokerContext>().LifestylePerWebRequest(),
                Component.For<IUserRepository>().ImplementedBy<UserNHiberanteRepository>().LifestyleSingleton(),
                Component.For<IProjectRepository>().ImplementedBy<ProjectNHiberanteRepository>().LifestyleSingleton(),
                Component.For<ISessionProvider>().ImplementedBy<SessionProvider>().LifestyleSingleton(),
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                );
        }
    }
}
using BugManagement.Logic.ILogic;
using BugManagement.Logic.Logic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugManagement.Logic
{
    public class LogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IBugLogic>().ImplementedBy<BugLogic>().LifestylePerWebRequest(),
                Component.For<IBugTypeLogic>().ImplementedBy<BugTypeLogic>().LifestylePerWebRequest(),
                Component.For<ICauseBugDeveloperLogic>().ImplementedBy<CauseBugDeveloperLogic>().LifestylePerWebRequest(),
                Component.For<IDeveloperLogic>().ImplementedBy<DeveloperLogic>().LifestylePerWebRequest(),
                Component.For<IProjectLogic>().ImplementedBy<ProjectLogic>().LifestylePerWebRequest(),
                Component.For<IUserLogic>().ImplementedBy<UserLogic>().LifestylePerWebRequest(),
                Component.For<IDocumentLogic>().ImplementedBy<DocumentLogic>().LifestylePerWebRequest()
                );
        }
    }
}

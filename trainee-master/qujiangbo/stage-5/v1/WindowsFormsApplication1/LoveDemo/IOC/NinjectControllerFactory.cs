using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LoveDemo.DomainModels;
using Ninject;
using LoveDemo.Models;

namespace LoveDemo.IOC
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
        }

        public NinjectControllerFactory(IKernel ninjectKernel)
        {
            _ninjectKernel = ninjectKernel;
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<ILoveRepository>().To<FakeLoveRepository>();
        }
    }

    public class FakeLoveRepository : ILoveRepository
    {
        public List<Person> GetLoversByName(string name)
        {
            return new List<Person>
            {
                new Person {Name = "Lucy",Age = 24},
                new Person { Name = "Lily",Age = 28}
            };
        }
    }
}
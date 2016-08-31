using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoveDemo.Controllers;
using NUnit.Framework;
using LoveDemo.DomainModels;
using LoveDemo.Models;
using NUnit.Framework.Internal;

namespace LoveDmeoTest
{
    [TestFixture]
    public class HomeControllerTest
    {
        private static readonly FakeLoveRepository FakeRepository=new FakeLoveRepository();
        private readonly HomeController _homeController =new HomeController(FakeRepository);

        [Test]
        public void SendTest()
        {
            //Act

            //Arrange
            var result = _homeController.Send(new Person {Name = "Bill"});
            //Assert
            Assert.AreEqual(result,2);
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

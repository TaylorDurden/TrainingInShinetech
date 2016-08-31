using System;
using System.Reflection;
using NUnit.Framework;
using NHibernate;
using NHibernate.Cfg;
using WebApplication1;

namespace ClassLibrary1
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void FactoryTest()
        {
            var cfg = new Configuration();
            var str = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            cfg.Configure(
                "D:/projectDemo/WebApplication1/WebApplication1/Nhibernate.cfg.xml");
            var sessionFactory = cfg.BuildSessionFactory();//建立Session工厂
            var session = sessionFactory.OpenSession();//打开Session
            var myUser = new User
            {
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERETFhwXExQaFR"
            };
            session.Save(myUser);
            session.Flush();
            //session.Delete(myUser);
        }
    }
}

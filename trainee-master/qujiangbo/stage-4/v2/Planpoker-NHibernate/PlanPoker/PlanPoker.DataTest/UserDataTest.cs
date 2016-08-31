using NUnit.Framework;
using NHibernate.Cfg;
using PlanPoker.Data.ModelsNHibernate;

namespace PlanPoker.DataTest
{
    [TestFixture]
    public class UserDataTest
    {

        [Test] 
        public void FactoryTest()
        {
            var cfg = new Configuration();
            cfg.Configure(
                "D:/projectDemo/qujiangbo/traineeCurrent/trainee/qujiangbo/stage-4/v2/Planpoker-NHibernate/PlanPoker/PlanPoker.Data/Nhibernate.cfg.xml");
            var sessionFactory = cfg.BuildSessionFactory();//建立Session工厂
            var session = sessionFactory.OpenSession();//打开Session
            var myUser = new User
            {
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = ""
            };
            session.Flush();
            //session.Save(myUser);
            //session.Delete(myUser);
        }
    }
}

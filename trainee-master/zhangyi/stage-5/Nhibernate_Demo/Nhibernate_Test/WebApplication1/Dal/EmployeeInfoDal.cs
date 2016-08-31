using System.Collections.Generic;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using WebApplication1.Models;

namespace WebApplication1.Dal
{
    public class EmployeeInfoDal
    {
        ISessionFactory _sessionFactory;
        /// <summary> 
        /// Method to create session and manage entities 
        /// </summary> 
        /// <returns></returns> 
        ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                var cgf = new Configuration();
                var data = cgf.Configure(HttpContext.Current.Server.MapPath(@"\Models\NHibernate\Configuration\nhibernate.cfg.xml"));
                cgf.AddDirectory(new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(@"\Models\NHibernate\Mappings\")));
                _sessionFactory = data.BuildSessionFactory();
            }
            return _sessionFactory.OpenSession();
        }
        public IList<EmployeeInfo> GetEmployees()
        {
            IList<EmployeeInfo> employees;
            using (ISession session = OpenSession())
            {
                //NHibernate query 
                IQuery query = session.CreateQuery("from EmployeeInfo");
                employees = query.List<EmployeeInfo>();
            }
            return employees;
        }
        public EmployeeInfo GetEmployeeById(int id)
        {
            EmployeeInfo emp;
            using (ISession session = OpenSession())
            {
                emp = session.Get<EmployeeInfo>(id);
            }
            return emp;
        }
        public int CreateEmployee(EmployeeInfo emp)
        {
            int empNo = 0;
            using (ISession session = OpenSession())
            {
                //Perform transaction 
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Save(emp);
                    tran.Commit();
                }
            }
            return empNo;
        }
        public void UpdateEmployee(EmployeeInfo emp)
        {
            using (ISession session = OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Update(emp);
                    tran.Commit();
                }
            }
        }
        public void DeleteEmployee(EmployeeInfo emp)
        {
            using (ISession session = OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Delete(emp);
                    tran.Commit();
                }
            }
        }
    }
}
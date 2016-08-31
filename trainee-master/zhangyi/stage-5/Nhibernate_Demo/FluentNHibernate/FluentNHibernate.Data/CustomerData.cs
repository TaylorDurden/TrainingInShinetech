using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Domain.Models;
using FluentNHibernate.Testing.Values;
using NHibernate;

namespace FluentNHibernate.Data
{
    public class CustomerData
    {
        public bool AddCustomer(Customer customer)
        {
            var session = FluentNHibernateHelper.GetSession();
            using (var trans = session.BeginTransaction())
            {

                session.SaveOrUpdate(customer);
                session.Flush();
                trans.Commit();
                return true;
            }

        }

        public List<Customer> GetCustomers()
        {
            var session = FluentNHibernateHelper.GetSession();
            using (var trans = session.BeginTransaction())
            {

                IQuery query = session.CreateQuery("from Customer");
                var customers = query.List<Customer>().ToList();
                return customers;
            }
        }
    }
}
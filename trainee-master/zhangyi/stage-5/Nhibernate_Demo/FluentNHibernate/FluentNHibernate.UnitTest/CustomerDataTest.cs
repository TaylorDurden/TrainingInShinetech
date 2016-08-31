using System;
using FluentNHibernate.Data;
using FluentNHibernate.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentNHibernate.UnitTest
{
    [TestClass]
    public class CustomerDataTest
    {
        private readonly CustomerData _customerData;
        public CustomerDataTest()
        {
            _customerData = new CustomerData();
        }
        [TestMethod]
        public void AddCustomerTest()
        {
            //Arrange
            var customer = new Customer()
            {
                Version = 2,
                CustomerName = "wolfy",
                CustomerAddress = "中国 北京"
            };

            //Act
            var result = _customerData.AddCustomer(customer);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void GetCustomersTest()
        {
            //Arrange
            

            //Act
            var result = _customerData.GetCustomers();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count>0);

        }
    }
}

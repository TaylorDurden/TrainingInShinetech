using System;
using System.Collections.Generic;
using System.Threading;

namespace NewFeaturesDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var customer=new Customer();

            //var strName = string.Format("FirstName is {0}, LastName is {1}", customer.FirstName, customer.LastName);

            //auto properties initialization
            //getter only auto properties

            var strName = $"FirstName is {customer.FirstName}, LastName is {customer.LastName}.";
            Console.WriteLine(strName);
            Thread.Sleep(1000);

            //lambda表达式用作属性

            Console.WriteLine(customer.ToString());
            Thread.Sleep(1000);

            //object initialization with index

            var numbers=new Dictionary<int,string>
            {
                [0]="zero",
                [1]="one",
                [5]="five",
                [7]="seven"
            };
            Console.WriteLine(numbers.Count);

            //using static

            Console.WriteLine(Math.Sqrt(6*6+8*8));


            
            //try
            //{
            //    var numbersException = new Dictionary<int, string>
            //    {
            //        [0] = "zero",
            //        [1] = "one",
            //        [5] = "five",
            //        [7] = "seven"
            //    };
            //}
            //catch (ArgumentNullException e)
            //{
            //    if (e.ParamName == "customer")
            //    {
            //        Console.WriteLine("");
            //    }
            //}

            

            Console.ReadKey();
        }
    }
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
        public string FirstName { get; set; } = "Bill";
        public string LastName { get; set; } = "Gates";

        public override string ToString() => $"FirstName is {FirstName}, LastName is {LastName}";
    }
}

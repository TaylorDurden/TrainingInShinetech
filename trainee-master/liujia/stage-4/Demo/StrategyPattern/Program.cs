
using System;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var operation = new InterestOperation(new PersonalTaxStrategy());
            Console.WriteLine("个人所得税为：{0}",operation.GetTax(5000));
            operation = new InterestOperation(new EnterPriseTaxStrategy());
            Console.WriteLine("公司所得税为：{0}", operation.GetTax(5000));

            Console.ReadKey();
        }
    }
}

using System;

namespace Proxy
{
    class Program
    {
        //给某一个对象提供一个代理,并由代理对象控制对原有对象的引用.
        static void Main(string[] args)
        {
            //创建代理对象并发出请求
            var proxy = new Friend();
            proxy.BugProduct();

            Console.ReadKey();
        }
    }
}

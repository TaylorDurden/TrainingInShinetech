using System;
using System.Threading;

namespace delegateDemo
{
    internal class Program
    {
        public delegate void GreetDelegate(string name);

        public delegate string MyDelegate(string name);
        private static void Main(string[] args)
        {
            //GreetPeople("Bill", EnglishGreeting);
            //GreetPeople("张三",ChineseGreeting);

            var delegateGreet = new GreetDelegate(EnglishGreeting);
            delegateGreet += ChineseGreeting;
            //delegateGreet += ChineseGreeting;
            //GreetPeople("Bill", delegateGreet);
            //delegateGreet -= ChineseGreeting;
            GreetPeople("Bill", delegateGreet);
            delegateGreet("Bill Gates");

            //ThreadMessgae("Main thread");
            //var myDelegate=new MyDelegate(Hello);
            //var result = myDelegate.BeginInvoke("Jimbo", null, null);
            //while (!result.IsCompleted)
            //{
            //    Console.WriteLine("Main thread do work!");
            //}
            //Console.WriteLine($"{myDelegate.EndInvoke(result)}");

            Console.ReadKey();
        }

        private static string Hello(string name)
        {
            ThreadMessgae("Async thread");
            Thread.Sleep(2000);
            return "Hello," + name;
        }

        private static void ThreadMessgae(string message)
        {
            Console.WriteLine($"{message}  ThreadId is {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void GreetPeople(string name, GreetDelegate greetDelegate)
        {
            greetDelegate(name);
        }

        private static void EnglishGreeting(string name)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello,"+name);
        }

        private static void ChineseGreeting(string name)
        {
            Thread.Sleep(1000);
            Console.WriteLine("您好，"+name);
        }
    }
}

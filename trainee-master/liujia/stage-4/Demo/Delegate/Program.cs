
using System;

namespace Delegate
{
    public delegate void GreetingDelegate(string name);
    class Program
    {
        static void Main(string[] args)
        {
            //委托使用"+="绑定一个方法，使用"-="取消一个方法的绑定
            //GreetPeople("jack",EnglishGreeting);
            //GreetPeople("jack", ChineseGreeting);

            //GreetingDelegate delegate1;
            //delegate1 = EnglishGreeting;
            //delegate1 += ChineseGreeting;

            //delegate1("Jack");        

            //delegate1 -= EnglishGreeting;
            //delegate1("Jimmy");


            var greetManager = new GreetingManager();
            greetManager.delegate1 += EnglishGreeting;
            greetManager.delegate1 += ChineseGreeting;
            greetManager.GreetPeople("leo");
            Console.ReadKey();
        }

        private static void EnglishGreeting(string name) {
            Console.WriteLine("Morning:{0}",name);
        }

        private static void ChineseGreeting(string name) {
            Console.WriteLine("早上好:{0}",name);
        }

        
    }

    public class GreetingManager
    {
        public event GreetingDelegate delegate1;
        public void GreetPeople(string name)
        {
            if (delegate1 != null) {
                delegate1(name);
            }
        }        
    }
}

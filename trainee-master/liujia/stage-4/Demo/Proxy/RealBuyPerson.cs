using System;

namespace Proxy
{
    public class RealBuyPerson:Person
    {
        public override void BugProduct()
        {
            Console.WriteLine("帮我买一个IPhone和一台苹果电脑");
        }
    }
}

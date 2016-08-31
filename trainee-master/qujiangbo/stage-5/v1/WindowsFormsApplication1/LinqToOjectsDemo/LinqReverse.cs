using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqReverse
    {
        public void Resovrts()
        {
            Console.WriteLine("-----------------Linq to reverse demo beigin-------------------");
            var str = "Reverse方法，用于反转序列中元素的顺序.";
            Console.WriteLine(str);
            var strReverse = str.ToCharArray().Reverse();
            foreach (var s in strReverse)
            {
                Console.Write(s);
            }
            Console.WriteLine("\n-----------------Linq to reverse demo end-------------------\n");
        }
    }
}

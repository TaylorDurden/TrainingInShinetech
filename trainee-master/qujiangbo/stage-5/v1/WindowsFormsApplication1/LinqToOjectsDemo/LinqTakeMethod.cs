using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqTakeMethod
    {
        /// <summary>
        /// Take方法用于从一个序列的开头返回指定数量的元素
        /// </summary>
        public void LinqToTakeMethod()
        {
            Console.WriteLine("-----------------Linq to take method beigin-------------------");

            string[] names = { "林晚荣", "洛凝", "董巧巧", "依莲", "安碧如" };

            //Take方法输出前三个元素
            foreach (var name in names.Take(3))
            {
                Console.WriteLine(name);
            }
            var queryResult = from name in names
                              where name.Length == 2
                              select name;
            
            //输出结果中的第一个元素
            foreach (var query in queryResult.Take(1))
            {
                Console.WriteLine($"{query}");
            }

            Console.WriteLine("-----------------Linq to take method end-------------------\n");
        }
    }
}

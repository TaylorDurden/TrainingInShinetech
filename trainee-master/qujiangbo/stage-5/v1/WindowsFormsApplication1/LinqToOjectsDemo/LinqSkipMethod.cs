using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqSkipMethod
    {
        /// <summary>
        /// Skip方法用于跳过序列中指定数量的元素，然后返回剩余的元素
        /// </summary>
        public void LinqToSkipMehtod()
        {
            Console.WriteLine("-----------------Linq to skip method beigin-------------------");
            string[] names = { "林晚荣", "洛凝", "董巧巧", "依莲", "安碧如" };

            foreach (var name in names.Skip(2))
            {
                Console.WriteLine(name);
            }

            var queryResult = from name in names
                where name.Length == 3
                select name;
            Console.WriteLine("-----------------Linq to skip method-------------------\n");
            foreach (var query in queryResult.Skip(1))
            {
                Console.WriteLine($"{query}");
            }

            Console.WriteLine("-----------------Linq to skip method end-------------------\n");
        }
    }
}

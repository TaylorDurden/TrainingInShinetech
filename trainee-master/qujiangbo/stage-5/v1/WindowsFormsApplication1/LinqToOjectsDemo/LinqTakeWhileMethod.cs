using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqTakeWhileMethod
    {
        /// <summary>
        /// TakeWhile方法 用于获取序列中从开头算起复合条件的元素，直到遇到不符合条件的元素为止
        /// </summary>
        public void LinqToTakeWhileMethod()
        {
            Console.WriteLine("-----------------Linq to takewhile method beigin-------------------");
            string[] names = { "洛凝",  "董巧巧", "林晚荣", "依莲", "安碧如" };
            

            //var queryResult = names.TakeWhile(x => x.Length == 2);

            var queryResult = names.TakeWhile((n,i)=>n.Length<=4 && i<4);

            foreach (var query in queryResult)
            {
                Console.WriteLine($"{query}");
            }

            Console.WriteLine("-----------------Linq to takewhile method end-------------------\n");
        }
    }
}

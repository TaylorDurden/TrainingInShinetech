using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    /// <summary>
    /// SkipWhile方法用于只要满足指定的条件，就跳过序列中的元素 ，然后返回剩余元素。
    /// </summary>
    public class LinqSkipWhileMethod
    {
        public void LinqToSkipWhileMethod()
        {
            Console.WriteLine("-----------------Linq to skipWhile method beigin-------------------");
            string[] names = { "洛2", "林晚荣", "洛凝", "董巧巧", "依莲", "安碧如" };

            //var queryResult = names.SkipWhile(x=>x.Length==2);

            var queryResult = names.SkipWhile((n,i)=>n.Length<4 && i<5);
            foreach (var query in queryResult)
            {
                Console.WriteLine($"{query}");
            }



            Console.WriteLine("-----------------Linq to skipWhile method end-------------------\n");
        }
    }
}

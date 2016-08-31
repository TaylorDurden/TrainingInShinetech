using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqPaginateDemo
    {
        public void LinqToPaging()
        {
            Console.WriteLine("-----------------Linq to page demo beigin-------------------");
            var ipage = 0;
            var pageSize = 3;

            string[] names = { "洛2", "林晚荣", "洛凝", "董巧巧", "依莲", "安碧如","张飞","赵云","关羽" };

            Console.WriteLine($"输出第{ipage}页记录");

            var queryResult = names.Skip(ipage*pageSize).Take(pageSize);

            foreach (var query in queryResult)
            {
                Console.WriteLine(query);
            }

            ipage++;

            Console.WriteLine($"输出第{ipage}页记录");
            var queryResult2=names.Skip(ipage * pageSize).Take(pageSize);

            foreach (var query2 in queryResult2)
            {
                Console.WriteLine(query2);
            }

            ipage++;

            Console.WriteLine($"输出第{ipage}页记录");
            var queryResult3 = names.Skip(ipage*pageSize).Take(pageSize);

            foreach (var query3 in queryResult3)
            {
                Console.WriteLine(query3);
            }

            Console.WriteLine("-----------------Linq to page demo end-------------------\n");
        }
    }
}

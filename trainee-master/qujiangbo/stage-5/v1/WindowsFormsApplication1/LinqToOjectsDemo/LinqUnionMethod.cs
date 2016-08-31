using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    public class LinqUnionMethod
    {
        /// <summary>
        /// Union方法用于合并两个序列，并去掉重复元素。
        /// </summary>
        public void LinqToUnion()
        {
            Console.WriteLine("-----------------Linq to union demo beigin-------------------");
 
            string[] girls1 = { "萧玉若", "洛凝", "萧玉霜", "秦仙儿" };
            string[] girls2 = { "月牙儿", "秦仙儿", "洛凝", "萧玉霜" };

            var girls = girls1.Union(girls2);
            Console.WriteLine("合并后的序列：");
            foreach (var girl in girls)
            {
                Console.WriteLine(girl);
            }
            Console.WriteLine("-----------------Linq to union demo end-------------------\n");
        }

        /// <summary>
        /// Concat方法用于连接两个序列，与，Union方法不同的是不会过滤掉重复的元素
        /// </summary>
        public void LinqToConcat()
        {
            Console.WriteLine("-----------------Linq to concat demo beigin-------------------");

            string[] girls1 = { "萧玉若", "洛凝", "萧玉霜", "秦仙儿" };
            string[] girls2 = { "月牙儿", "秦仙儿", "洛凝", "萧玉霜" };

            var girls = girls1.Concat(girls2);

            foreach (var girl in girls)
            {
                Console.WriteLine(girl);
            }
            Console.WriteLine("-----------------Linq to concat demo end-------------------\n");
        }

        /// <summary>
        /// 用于生成两个序列的交集。生成交集，就是把2个序列中相同的元素提取出来，建立一个新的序列
        /// </summary>
        public void LinqToIntersect()
        {
            Console.WriteLine("-----------------Linq to intersect demo beigin-------------------");

            string[] girls1 = { "萧玉若", "洛凝", "萧玉霜", "秦仙儿" };
            string[] girls2 = { "月牙儿", "秦仙儿", "洛凝", "萧玉霜" };

            var girls = girls1.Intersect(girls2);

            foreach (var girl in girls)
            {
                Console.WriteLine(girl);
            }

            Console.WriteLine("-----------------Linq to intersect demo end-------------------\n");
        }

        /// <summary>
        /// 用于生成两个序列的差集，生成差集，就是把两个序列中不同的元素提取出来，建立一个新的序列
        /// </summary>
        public void LinqToExcept()
        {
            Console.WriteLine("-----------------Linq to except demo beigin-------------------");

            string[] girls1 = { "萧玉若", "洛凝", "萧玉霜", "秦仙儿" };
            string[] girls2 = { "月牙儿", "秦仙儿", "洛凝", "萧玉霜" };

            var girls = girls1.Except(girls2);

            var girlss = girls2.Except(girls1);

            foreach (var girl in girls)
            {
                Console.WriteLine(girl);
            }
            Console.WriteLine("-------------------------------------");
            foreach (var girl in girlss)
            {
                Console.WriteLine(girl);
            }

            Console.WriteLine("-----------------Linq to except demo end-------------------\n");
        }

        /// <summary>
        /// 用于生成指定范围的整数序列，在BS程序中，经常需要分页显示，在页面中需要显示页面号码的链接，用这个方法可以生成页码数组。
        /// </summary>
        public void LinqToRange()
        {
            Console.WriteLine("-----------------Linq to range demo beigin-------------------");

            const int istart = 16;
            const int iend = 30;

            var pages = Enumerable.Range(istart, iend - istart + 1);
            Console.WriteLine("输出页码：");
            foreach (var page in pages)
            {
                Console.WriteLine(page);
            }
            

            Console.WriteLine("-----------------Linq to range demo end-------------------\n");
        }

        public void LinqToList()
        {
            string[] strarray = { "出云公主", "乖巧人儿", "萧家二小姐", "霓裳公主" };

            var queryResult = from str in strarray
                where str.IndexOf("公主", StringComparison.Ordinal) > -1
                select str;

            var lists = queryResult.ToList();

            Console.WriteLine($"strarray 类型：{strarray.GetType().Name}");
            Console.WriteLine($"query 类型：{queryResult.GetType().Name}");
            Console.WriteLine($"lst 类型：{lists.GetType().Name}");

            

            Console.WriteLine("\nlst内容：");
            foreach (var list in lists)
            {
                Console.WriteLine(list);
            }

        }
    }
}

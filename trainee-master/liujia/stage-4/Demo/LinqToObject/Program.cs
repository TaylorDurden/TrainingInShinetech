using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToObject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var students = new List<Student>();
            var random = new Random();
            for (var i = 0; i < 50; i++)
            {
                var student = new Student
                {
                    Id = i.ToString("00"),
                    English = random.Next(0, 100),
                    Math = random.Next(0, 100),
                    Computer = random.Next(0, 100)
                };
                students.Add(student);
            }
            //投影 （count用法）
            var count = students.Count(g => (g.Computer + g.English + g.Math) / 3 >= 60);
            Console.WriteLine("平均分60以上的人数:{0}人", count);

            //选择 
            //Select用法
            //var selects = students.Select(s => new { s.Id, s.English, s.Math });
            //var select = from s in students select new { s.Id, s.English, s.Math };

            //排序
            var list = from s in students
                       where s.Average > 60
                       orderby s.Average descending
                       select s;
            Console.WriteLine("平均分60以上的人有：");
            foreach (var student in list)
            {
                Console.Write("{0},",student.Id);
            }
            Console.WriteLine();
            //数据分页
            Console.WriteLine("下面是分页数据:");
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine("这是第:{0}页", i);
                Console.WriteLine("------------------------------------------------------------");
                var result = list.Skip(10).Take(10);
                foreach (var s in result)
                {
                    Console.WriteLine("id:{0}ave:{1}", s.Id, s.Average);
                }
                Console.WriteLine("------------------------------------------------------------");
            }

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToOjectsDemo
{
    public class LinqToArrayList
    {
        public IEnumerable<Student> LinqToArrayListTest()
        {
            var studentList=new ArrayList
            {
                new Student {Id = 1,Name = "Bill",Scoure = new[]{ 92, 82, 88, 72 } },
                new Student {Id = 2,Name = "Jack",Scoure = new[]{ 90, 82, 68, 72 } },
                new Student {Id = 3,Name = "Eric",Scoure = new[]{ 82, 82, 88, 72 } },
                new Student {Id = 4,Name = "Jimb",Scoure = new[]{ 78, 82, 83, 72 } }
            };

            var queryResult = from Student student in studentList
                where student.Scoure[0] > 85
                select student;

            return queryResult;
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Scoure { get; set; }
    }
}

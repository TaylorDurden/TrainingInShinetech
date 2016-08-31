
using System;
using System.Collections.Generic;
using System.Linq;

namespace Features
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GetAttributeInfo(typeof (OldClassOld));
            Console.WriteLine("==============");
            GetAttributeInfo(typeof (NewClass));

            //NULL检查运算符
            var points = new List<Point>() {new Point(5,1),new Point(3,4)};
            var fristx = points.FirstOrDefault()?.X;
            Console.WriteLine(fristx);
            
            var point = new Point();
            Console.WriteLine(point.Distance);

            //异常过滤器(Exception filters)
            try
            {
                //带索引的对象初始化器Index initializers
                var data = new Dictionary<int, string>
                {
                    [7] = "seven",
                    [9] = "nine",
                    [13] = "thirteen"
                };
                Console.WriteLine(data.Count);
                //在C#5.0中，await关键字是不能出现在catch和finnaly块中的。而在6.0中可以出现
            }
            catch (ArgumentNullException e)
            {
                //catch和finally 中的 await —— Await in catch and finally blocks
                if (e.ParamName == "user")
                {
                    Console.WriteLine("user can not be null");
                }
            }
            

            var user = new User
            {
                FirstName = "liu",
                LastName = "jia"
            };
            Console.WriteLine("user名字为:" + user.FullName);
            Console.WriteLine("user名字为:" + user);

            


            Console.ReadKey();
        }

        public static void GetAttributeInfo(Type t)
        {
            var myattribute = (OldAttribute)Attribute.GetCustomAttribute(t, typeof(OldAttribute));
            if (myattribute == null)
            {
                Console.WriteLine(t + "类中自定义特性不存在！");
            }
            else
            {
                Console.WriteLine("特性描述:{0}\n加入事件{1}", myattribute.Discretion, myattribute.Date);
            }
        }
    }
}

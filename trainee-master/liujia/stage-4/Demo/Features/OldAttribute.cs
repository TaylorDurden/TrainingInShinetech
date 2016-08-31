using System;

namespace Features
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class OldAttribute:Attribute
    {
        public string Discretion { get; set; }

        public DateTime Date;
        public OldAttribute(string discretion)
        {
            Discretion = discretion;
            Date = DateTime.Now;
        }
    }

    [Old("这个类将过期")]
    public class OldClassOld
    {
        public void OldTest()
        {
            Console.WriteLine("测试特性");
        }
    }

    public class NewClass : OldClassOld
    {
        public void NewTest()
        {
            Console.WriteLine("测试特性的继承");
        }
    }
}
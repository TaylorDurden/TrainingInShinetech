
using System;

namespace Builder
{
    //定义 将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示
    //适用场景 创建一个复杂对象，并且这个复杂对象由其各部分子对象通过一定的步骤组合而成
    class Program
    {
        //以组装电脑为例子
        static void Main(string[] args)
        {
            //创建指挥者和构造者
            var director = new Director();
            var builder1 = new ConcreateBuilder1();
            var builder2 = new ConcreateBuilder2();

            //老板让员工去组装第一台电脑
            director.Construct(builder1);            
            var computer = builder1.GetComputer();
            computer.Show();
            //老板让员工去组装第二台电脑
            director.Construct(builder2);
            computer = builder2.GetComputer();
            computer.Show();

            Console.ReadKey();
        }
    }
}

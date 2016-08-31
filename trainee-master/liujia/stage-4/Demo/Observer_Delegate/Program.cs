using System;

//观察者模式  一个目标物件管理所有相依于它的观察者物件，并且在它本身的状态改变时主动发出通知.
namespace Observer_Delegate
{
    //假设我们有个高档的热水器，我们给它通上电，当水温超过95度的时候：1、扬声器会开始发出语音，告诉你水的温度；2、液晶屏也会改变水温的显示，来提示水已经快烧开了
    class Program
    {
        static void Main(string[] args)
        {
            var heater = new Heater();
            heater.Boiled += new Alarm().MakeAlert;//给匿名对象注册方法
            heater.Boiled += new Heater.BoiledEventHandler(new Alarm().MakeAlert);//也可以这么注册
            heater.Boiled += new Display().ShowMsg;
            
            heater.BoilWater();
            Console.ReadKey();
        }
    }
}

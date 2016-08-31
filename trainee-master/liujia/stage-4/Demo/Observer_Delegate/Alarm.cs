using System;
namespace Observer_Delegate
{
    //警报器
    public class Alarm
    {
        /// <summary>
        /// 发出语音警报
        /// </summary>
        public void MakeAlert(Object sender, Heater.BoiledEventArgs e)
        {
            var heater = (Heater)sender;
            Console.WriteLine("Alarm:{0}-{1}", heater.type, heater.area);
            Console.WriteLine("Alarm:滴滴滴，水已经{0}度了。", e.temperature);
            Console.WriteLine();
        }
    }
}

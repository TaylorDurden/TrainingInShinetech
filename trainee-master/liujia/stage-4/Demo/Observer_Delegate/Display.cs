using System;

namespace Observer_Delegate
{
    //显示器
    public class Display
    {
        /// <summary>
        /// 显示水温
        /// </summary>
        public void ShowMsg(Object sender, Heater.BoiledEventArgs e)
        {
            var heater = (Heater)sender;
            Console.WriteLine("Display:{0}-{1}", heater.type, heater.area);
            Console.WriteLine("Display:水快开了，当前温度：{0}度。", e.temperature);
            Console.ReadKey();
        }
    }
}

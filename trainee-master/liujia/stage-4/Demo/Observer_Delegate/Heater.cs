using System;

namespace Observer_Delegate
{
    // 热水器
    public class Heater
    {
        private int temperature;//水温
        public string type = "RealFire 001";//型号
        public string area = "China Xian";//产地

        public delegate void BoiledEventHandler(Object sender, BoiledEventArgs e);
        public event BoiledEventHandler Boiled;

        public class BoiledEventArgs : EventArgs
        {
            public readonly int temperature;
            public BoiledEventArgs(int temperature)
            {
                this.temperature = temperature;
            }
        }

        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            if (Boiled != null)
            {
                Boiled(this, e);
            }
        }

        /// <summary>
        /// 烧水
        /// </summary>
        public void BoilWater()
        {
            for (var i = 0; i <= 100; i++)
            {
                temperature = i;

                if (temperature > 95)
                {
                    var e = new BoiledEventArgs(temperature);
                    OnBoiled(e);
                }
            }
        }
    }
}

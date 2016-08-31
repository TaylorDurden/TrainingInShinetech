using System;

namespace Decorator
{
    //手机挂件
    public class Accessories:Decorator
    {
        public Accessories(Phone p) : base(p) {
        }

        public override void Print()
        {
            base.Print();

            //添加新的行为
            AddAccessories();
        }

        public void AddAccessories() {
            Console.WriteLine("现在苹果手机有漂亮的挂件了");
        }
    }
}

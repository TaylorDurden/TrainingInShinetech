using System;

namespace Decorator
{
    //手机贴膜
    public class Sticker:Decorator
    {
        public Sticker(Phone p) : base(p) {
        }

        public override void Print()
        {
            base.Print();

            //添加新的行为
            AddSticker();
        }

        public void AddSticker() {
            Console.WriteLine("现在苹果手机有贴膜了");
        }
    }
}

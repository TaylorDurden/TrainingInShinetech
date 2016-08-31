using System;

namespace Decorator
{
    //装饰抽象类,要让装饰完全取代抽象组件，所以必须继承自Phone
    public abstract class Decorator:Phone
    {
        private Phone phone;

        public Decorator(Phone p) {
            phone = p;
        }

        public override void Print()
        {
            if (phone != null) {
                phone.Print();
            }
        }
    }
}

using System;

namespace Decorator
{
    public class Shell :Decorator
    {
        public Shell(Phone p) : base(p) {
        }

        public override void Print()
        {
            base.Print();

            AddShell();
        }

        public void AddShell() {
            Console.WriteLine("现在苹果手机有外壳了");
        }
    }
}

using System;

namespace Features
{
    [Obsolete("该类已过时！",true)]
    public class OldClass
    {
        [method:Obsolete("该方法已经过时！")]
        public void OldMethod()
        {
            Console.WriteLine("该方法已经过时！");
        }
    }
}
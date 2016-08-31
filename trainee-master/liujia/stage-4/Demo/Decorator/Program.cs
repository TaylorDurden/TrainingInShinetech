using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var phone = new ApplePhone();

            var applePhoneWithSticker = new Sticker(phone);
            applePhoneWithSticker.Print();
            Console.WriteLine("==========================");

            var applePhoneWithAccessories = new Accessories(phone);
            applePhoneWithAccessories.Print();
            Console.WriteLine("==========================");

            var applePhoneWithShell = new Shell(phone);
            applePhoneWithShell.Print();
            Console.WriteLine("==========================");

            var sticker = new Sticker(phone);
            var accessories = new Accessories(sticker);
            var shell = new Shell(accessories);
            shell.Print();
            
            Console.ReadKey();
        }
    }
}

using System;
using System.Linq;

namespace LinqToOjectsDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var linqStrOject = new LinqAndString();


            //get keywords appear count
            Console.WriteLine($"关键字packages出现的次数：{linqStrOject.GetStringAppearCount().Count()}");

            //get digit from const string
            foreach (var item in linqStrOject.GetDigitFromString())
            {
                Console.WriteLine($"{item}");
            }

            //linq to reflection
            var linqToReflection = new LinqToReflection();
            foreach (var methods in linqToReflection.LinqToReflectionTest())
            {
                Console.WriteLine($"{methods.Key}");
                foreach (var method in methods)
                {
                    Console.WriteLine($"{method}");
                }
            }


            //Linq to file info
            var linqToFileInfo = new LinqToFileInfo().LinqToFileInfoTest();
            foreach (var item in linqToFileInfo)
            {
                Console.WriteLine($"{item.Name}:{item.CreationTime}");
            }

            //linq to array list
            var linqToArrayList = new LinqToArrayList().LinqToArrayListTest();
            foreach (var item in linqToArrayList)
            {
                Console.WriteLine($"{item.Name}:{item.Scoure[0]}");
            }

            //Linq to take method
            var linqTakeMethod=new LinqTakeMethod();

            linqTakeMethod.LinqToTakeMethod();

            //Linq to takeWhile method
            var linqTakeWhileMethod=new LinqTakeWhileMethod();

            linqTakeWhileMethod.LinqToTakeWhileMethod();

            //linq to skip method
            var linqSkipMethod=new LinqSkipMethod();
            linqSkipMethod.LinqToSkipMehtod();

            //linq to skipWhile method
            var linqSkipWhileMethod=new LinqSkipWhileMethod();

            linqSkipWhileMethod.LinqToSkipWhileMethod();

            //Linq to paginate demo
            var linqPaginate = new LinqPaginateDemo();
            
            linqPaginate.LinqToPaging();

            //linq to reverse
            var linqReverse = new LinqReverse();

            linqReverse.Resovrts();
            
            //linq to union
            var linqUnion = new LinqUnionMethod();
            linqUnion.LinqToUnion();

            linqUnion.LinqToConcat();

            linqUnion.LinqToIntersect();

            linqUnion.LinqToExcept();

            linqUnion.LinqToRange();

            linqUnion.LinqToList();

            Console.ReadKey();
        }
    }
}

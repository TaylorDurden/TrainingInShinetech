using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Threading;

namespace TPL_PLinq_lambdaDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ParllelForeachLoop();

            //ParllelForLoop();

            //SpeedUpParallelForeachLoop();

            //CompareRunTime();

            //TestParllel();

            //HandleExceptionParallelLoop();

            Console.ReadKey();
        }

        private static void ParllelForeachLoop()
        {
            int[] array = { 4, 1, 6, 2, 9, 5, 10, 3 };
            var sum = 0;
            try
            {
                Parallel.ForEach(array, () => 0, (element, loopState, localSum) =>
                {
                    localSum += element;
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId},{element},{localSum}");
                    return localSum;
                }, localSum => Interlocked.Add(ref sum, localSum));

                Console.ReadLine();
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"Parallel.ForEach has thrown an exception. This was not expected. \n{e}");

                Console.ReadLine();
            }
        }

        private static void ParllelForLoop()
        {
            var nums = Enumerable.Range(0, 10000).ToArray();
            long sum = 0;

            Parallel.For(0, nums.Length, () =>0, (element, loopState, localSum) =>
            {
                localSum += nums[element];
                return localSum;
            },localSum=>Interlocked.Add(ref sum,localSum));

            Console.WriteLine($"The total is {sum}");
            Console.ReadKey();
        }

        private static void SpeedUpParallelForeachLoop()
        {
            var nums = Enumerable.Range(0, 1000000).ToArray();

            var rangePartitioner = Partitioner.Create(0, nums.Length);

            var results = new double[nums.Length];

            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                for (var i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = nums[i] * Math.PI;
                }
            });

            Console.WriteLine("Operation complete. Print results? y/n");
            var input = Console.ReadKey().KeyChar;
            if (input != 'y' && input != 'Y') return;
            foreach (var d in results)
            {
                Console.Write($"{d}");
            }
        }

        private static void CompareRunTime()
        {
            var stuList = new List<Student>();
            for (var i = 0; i < 100; i++)
            {
                stuList.Add(new Student
                {
                    Name = "zhangsan" + i,
                    Age = i,
                    Sex = i % 2 == 0 ? "男" : "女"
                });
            }
            var startTime = DateTime.Now;
            Loop1(stuList);
            var resultTime = DateTime.Now - startTime;
            Console.WriteLine($"一般for循环耗时：{resultTime}");

            startTime = DateTime.Now;
            Loop2(stuList);
            resultTime = DateTime.Now - startTime;
            Console.WriteLine($"一般foreach循环耗时：{resultTime}");

            startTime = DateTime.Now;
            Loop3(stuList);
            resultTime = DateTime.Now - startTime;
            Console.WriteLine($"并行for循环耗时：{resultTime}");

            startTime = DateTime.Now;
            Loop4(stuList);
            resultTime = DateTime.Now - startTime;
            Console.WriteLine($"并行foreach循环耗时：{resultTime}");

            startTime = DateTime.Now;
            Loop5(stuList);
            resultTime = DateTime.Now - startTime;
            Console.WriteLine($"加快并行foreach循环耗时：{resultTime}");
            
        }

        //普通的for循环
        private static void Loop1(IReadOnlyCollection<Student> source)
        {
            for (var i = 0; i < source.Count; i++)
            {
                Thread.Sleep(100);
            }
        }

        //普通的foreach循环
        private static void Loop2(IEnumerable<Student> source)
        {
            foreach (var item in source)
            {
                Thread.Sleep(100);
            }
        }

        //并行的for循环
        private static void Loop3(IReadOnlyCollection<Student> source)
        {
            Parallel.For(0, source.Count, item =>
            {
                Thread.Sleep(100);
            });
        }

        //并行的foreach循环
        private static void Loop4(IEnumerable<Student> source)
        {
            Parallel.ForEach(source, item =>
            {
                Thread.Sleep(100);
            });
        }

        //加快并行的foreach循环
        private static void Loop5(IEnumerable<Student> source)
        {
            var rangePartitioner = Partitioner.Create(0, source.Count());

            Parallel.ForEach(rangePartitioner, range =>
            {
                Thread.Sleep(100);
            });
        }

        private static void TestParllel()
        {
            var list = new List<int>(6000);
            for (var i = 0; i < 6000; i++)
            {
                list.Add(i);
            }
            //Parallel.ForEach(list, (p, state) => { Invoke(p); });
            Parallel.ForEach(list, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (p, state) => { Invoke(p); });
        }

        private static void Invoke(int i)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "------------" + i);
            Thread.Sleep(1000);
        }

        private static void HandleExceptionParallelLoop()
        {
            var data=new byte[500];
            var random=new Random();
            random.NextBytes(data);

            try
            {
                ProcessDataInParallel(data);
            }

            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    if (ex is ArgumentException)
                        Console.WriteLine(ex.Message);
                    else
                        throw ex;
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static void ProcessDataInParallel(byte[] data)
        {
            var exceptions = new ConcurrentQueue<Exception>();
            
            Parallel.ForEach(data, d =>
            {
                try
                {
                    if (d < 0x3)
                        throw new ArgumentException($"value is {d:x}. Elements must be greater than 0x3.");
                    else
                        Console.Write(d + " ");
                }                 
                catch (Exception e) { exceptions.Enqueue(e); }
            });
            
            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartitionersDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LoadBalancePartitionerProgram();

            //RangePartitionerProgram();

            Console.ReadKey();
        }

        /// <summary>
        /// Ranges the partitioner program.
        /// </summary>
        private static void RangePartitionerProgram()
        {
            var nums = Enumerable.Range(0, 100).ToArray();

            var rangePartitioner = Partitioner.Create(0, nums.Length);
            var results=new double[nums.Length];

            Parallel.ForEach(rangePartitioner, new ParallelOptions {MaxDegreeOfParallelism = 3}, (range, loopState) =>
            {
                for (var i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = Math.PI*nums[i];
                    Console.WriteLine(results[i]);
                }
            });
        }

        /// <summary>
        /// Loads the balance partitioner program.
        /// </summary>
        private static void LoadBalancePartitionerProgram()
        {
            var nums = Enumerable.Range(0, 100).ToArray();

            var rangePartitioner = Partitioner.Create(nums, true);

            var strQuery = from x in rangePartitioner.AsParallel()
                select Math.PI*x;
            strQuery.ForAll(ProcessData);
        }

        private static void ProcessData(double x)
        {
            Console.WriteLine($"{x}");
        }
    }
}

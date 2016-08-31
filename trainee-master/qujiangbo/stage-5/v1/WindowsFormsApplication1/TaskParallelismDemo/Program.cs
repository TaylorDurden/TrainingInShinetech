using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallelismDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //ImplicitlyCreateTask();

            //DisplayCreateTaskByNewTask();

            //DisplayCreateTaskByTaskRun();

            DisplayCreateTaskByTaskFactoryStartNew();

            CreateDetachedChildTask();//parent task can't wait child task completed
            Console.ReadKey();
        }

        private static void ImplicitlyCreateTask()
        {
            Parallel.Invoke(DoSomething,DoOtherSomething);
        }

        private static void DisplayCreateTaskByNewTask()
        {
            Thread.CurrentThread.Name = "Display create task by new task";
            var displayTask=new Task(DoSomething);
            displayTask.Start();

            Console.WriteLine($"Hello from thread {Thread.CurrentThread.Name}");

            //确保任务在控制台模式应用程序结束之前完成执行
            displayTask.Wait();
        }

        private static void DisplayCreateTaskByTaskRun()
        {
            Thread.CurrentThread.Name = "Display create task by task run";
            var displayTask = Task.Run(()=>Console.WriteLine("task run "));

            Console.WriteLine($"Hello from thread {Thread.CurrentThread.Name}");
            displayTask.Wait();
        }

        private static void DisplayCreateTaskByTaskFactoryStartNew()
        {
            Thread.CurrentThread.Name = "Display create task by task factory start new.";
            var displatTask = Task.Factory.StartNew(() => Console.WriteLine("task factroy start new,"));

            Console.WriteLine($"Hello from thread {Thread.CurrentThread.Name}");
            displatTask.Wait();
        }

        private static void CreateDetachedChildTask()
        {
            var parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task beginning.");
                var childTask = Task.Factory.StartNew(() =>
                {
                    Thread.SpinWait(5000);
                    Console.WriteLine("Child task completed.");
                });
            });
            parentTask.Wait();
            Console.WriteLine("Parent task completed.");
        }

        private static void DoSomething()
        {
            Console.WriteLine("Do something. ");
        }

        private static void DoOtherSomething()
        {
            Console.WriteLine("Do other something. ");
        }
    }
}

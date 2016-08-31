using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    public delegate void ShowValue();

    public class Name
    {
        private string instanceName;

        public Name(string name) {
            instanceName = name;
        }

        public void DisplayToConsole() {
            Console.WriteLine("测试委托1{0}",instanceName);
        }

        public void DisplayToWindows() {
            Console.WriteLine("测试委托2{0}",instanceName);
        }
    }

    public class DelegateShowValue
    {
        public event ShowValue eventShowValue;

        public void ShowValue() {
            if(eventShowValue != null){
                eventShowValue();
            }
        }

    }
    public class Program
    {
        public static void Main(string[] args)
        {
            //var name = new Name("leoliu");
            //var delegateShowValue = new DelegateShowValue();
            //delegateShowValue.eventShowValue += name.DisplayToConsole;
            //delegateShowValue.eventShowValue += name.DisplayToWindows;
            //delegateShowValue.ShowValue();

            //delegateShowValue.eventShowValue -= name.DisplayToWindows;
            //delegateShowValue.ShowValue();

            //Action showMethod = name.DisplayToConsole;
            //showMethod();

            //Action<object> action = (object obj) =>
            //{
            //    Console.WriteLine("Task={0},obj={1},Thread={2}", Task.CurrentId, obj.ToString(), Thread.CurrentThread.ManagedThreadId);
            //};
             
            //Task t1 = new Task(action, "alpha");
            //Task t2 = Task.Factory.StartNew(action, "beta");

            //t2.Wait();
            //t1.Start();
            

            //Console.WriteLine("t1 has been launched.(Main Thread={0})",Thread.CurrentThread.ManagedThreadId);

            //t1.Wait();

            //Task t3 = new Task(action,"gamma");

            //t3.RunSynchronously();

            //t3.Wait();


            Console.WriteLine("Press any key to start.Press 'c' to cancel");
            Console.ReadKey();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Task[] tasks = new Task[10];

            tasks[0] = Task.Factory.StartNew(() => DoSomeWork(1, token), token);

            tasks[1] = Task.Factory.StartNew(() =>
            {
                for (var i = 2; i < 10; i++) {
                    tasks[i] = Task.Factory.StartNew(iteration => DoSomeWork((int)iteration, token), i, token);
                }

                DoSomeWork(2, token);
            },token);

            Thread.Sleep(1000);

            if (Console.ReadKey().KeyChar == 'c') {
                tokenSource.Cancel();
                Console.WriteLine("Task cancellation requested.");

                try
                {
                    Task.WaitAll(tasks);
                }
                catch (AggregateException e) {
                    foreach (var v in e.InnerExceptions) {
                        Console.WriteLine("mgs:{0}",v.Message);
                    }
                }

                for (var i = 0; i < tasks.Length; i++) {
                    Console.WriteLine("task[{0}] status is now {1}",i,tasks[i].Status);
                }
            }
            Console.ReadLine();
        }

        public static void DoSomeWork(int taskNum,CancellationToken cancelToken)
        {
            if (cancelToken.IsCancellationRequested) {
                Console.WriteLine("We were canceled before we got started.");
                Console.WriteLine("Press Enter to quit.");
                cancelToken.ThrowIfCancellationRequested();
            }
            int maxIterations = 1000;
            for (var i = 0; i < maxIterations; i++) {
                var sw = new SpinWait();

                for (var j = 0; j < 3000; j++) {
                    sw.SpinOnce();
                }

                if (cancelToken.IsCancellationRequested) {
                    Console.WriteLine("bye from {0}",taskNum);
                    Console.WriteLine("Press enter to quit.");
                    cancelToken.ThrowIfCancellationRequested();
                }
            }
        }

    //    public static void Main(string[] args)
    //    {
    //        Console.WriteLine("同步实现");
    //        var results = new long[20];
    //        for (var i = 0; i < 20; i++)
    //        {
    //            long x = 1;
    //            for (var j = 1; j <= i; j++)
    //            {
    //                x *= j;
    //            }
    //            results[i] = x;
    //            Console.WriteLine(i + "计算完成");
    //        }
    //        for (var i = 0; i < 20; i++)
    //        {
    //            Console.WriteLine(results[i]);
    //        }
    //        Console.WriteLine("========================");
    //        Console.WriteLine("Linq方式");
    //        foreach (var result in
    //(from i in Enumerable.Range(0, 20)
    // select new Func<long>(() =>
    // {
    //     long x = 1;
    //     for (var j = 1; j <= i; j++)
    //     {
    //         x *= j;
    //     }
    //     Console.WriteLine(i + "计算完成");
    //     return x;
    // })()).ToList())
    //        {
    //            Console.WriteLine(result);
    //        }
    //        Console.WriteLine("========================");
    //        Console.WriteLine("PLinq方式");
    //        foreach (var result in 
    //            (from i in Enumerable.Range(0,20).AsParallel().AsOrdered()
    //                                select new Func<long>(() =>
    //                                {
    //                                    long x = i;
    //                                    for (var j = 1; j <= i; j++)
    //                                    {
    //                                        x *= j;
    //                                    }
    //                                    Console.WriteLine(i+"计算完成");
    //                                    return x;
    //                                })()).ToList())
    //        {
    //            Console.WriteLine(result);
    //        }
    //        Console.WriteLine("========================");
    //        Console.WriteLine("TPL方式");
    //        var tf = new TaskFactory();
    //        var t = tf.ContinueWhenAll(
    //            (from i in Enumerable.Range(0, 20)
    //                select tf.StartNew(() =>
    //                {
    //                    long x = 1;
    //                    for (var j = 1; j <= i; j++)
    //                    {
    //                        x *= j;
    //                    }
    //                    Console.WriteLine(i + "计算完成");
    //                    return x;
    //                })).ToArray(),
    //            tasks =>
    //            {
    //                foreach (var task in tasks)
    //                {
    //                    Console.WriteLine(task.Result);
    //                }
    //            });
    //        t.Wait();
    //        Console.WriteLine("========================");
            
    //        Console.WriteLine("线程安全");
    //        const int count = 100000;
    //        var value = 0;
    //        var sw = Stopwatch.StartNew();
    //        Parallel.For(0, count * 4, () => 0,
    //            (i, state, local) => local + 1,
    //            local => Interlocked.Add(ref value, local));
    //        sw.Stop();
    //        Console.WriteLine(sw.ElapsedMilliseconds + "ms");
    //        Console.WriteLine(value);
    //        Contract.Assert(value == count * 4, "Not thread safe at all!");
    //        Console.WriteLine("========================");

    //        Console.WriteLine("异步撤销");
    //        var cts = new CancellationTokenSource();
    //        ThreadPool.QueueUserWorkItem(obj =>
    //        {
    //            var token = (CancellationToken) obj;
    //            for (var i = 0; i < 10; i++)
    //            {
    //                Console.WriteLine(i);
    //                if (token.IsCancellationRequested)
    //                {
    //                    token.Register(() =>
    //                    {
    //                        for (int j = i; j >= 0; j--)
    //                        {
    //                            Console.WriteLine("撤销" + j);
    //                        }
    //                    });
    //                    break;
    //                }
    //                Thread.Sleep(100);
    //            }
    //        }, cts.Token);
    //        Thread.Sleep(500);
    //        cts.Cancel();
    //        Thread.Sleep(100);

    //        Console.ReadKey();
    //    }
    }
}

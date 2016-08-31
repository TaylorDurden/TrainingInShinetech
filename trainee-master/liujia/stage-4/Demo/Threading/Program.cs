using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    public class Program
    {
        private static bool _isDone = false;
        private static object _lock = new object();
        private static SemaphoreSlim _sem = new SemaphoreSlim(3);
        public static void Main(string[] args)
        {
            //var worker = new Worker();

            //// 另外构造函数接受的是对象的方法的名称
            //var workThread = new Thread(worker.DoWork);
            //workThread.Start();
            //Console.WriteLine("主线程：worker线程开始...");


            ////循环直到线程被激活，Thread的IsAlive这个属性表示线程是否为活动的
            //while (!workThread.IsAlive)
            //{
            //    // 将主线程暂停1毫秒，以允许worker这个线程完成自己的工作
            //    Thread.Sleep(1);
            //}

            //worker.RequestStop();
            ////使用Join这个方法来阻塞当前线程，直到对象的线程终止
            //workThread.Join();
            //Console.WriteLine("main thread:Worker thread has terminated.");


            //Console.WriteLine("我是一个线程:Thread Id{0}", Thread.CurrentThread.ManagedThreadId);
            //ThreadPool.QueueUserWorkItem(Go);
            //System.Threading.Tasks.Task.Run(() => { GoGoGo("", "", "");  });

            ////线程之间共享数据
            //new Thread(Done).Start();
            //new Thread(Done).Start();

            ////Semaphore 信号量  允许一定数量的线程同时访问
            //for (var i = 1; i <= 5; i++) {
            //    new Thread(Enter).Start(i);
            //}

            //async & await
            Console.WriteLine("Main Thread Id: {0}\r\n", Thread.CurrentThread.ManagedThreadId);
            Test();
            
            Console.ReadKey();
        }

        public static void Go(object data) {
            Console.WriteLine("我是另外一个线程:Thread Id{0}", Thread.CurrentThread.ManagedThreadId);
        }

        public static void GoGoGo(string arg1, string arg2, string arg3) {
            Console.WriteLine("这是一个多参数线程:Thread ID{0}",Thread.CurrentThread.ManagedThreadId);
        }

        public static void Done() {
            //锁
            lock (_lock) {
                if (!_isDone)
                {
                    Console.WriteLine("Done共享数据");
                    _isDone = true;
                }
                else
                {
                    Console.WriteLine("共享数据停止");
                }
            }

        }

        public static void Enter(object id) {
            Console.WriteLine(id+"开始排队。。。");
            _sem.Wait();
            Console.WriteLine(id + "开始执行。。。");
            Thread.Sleep(1000*(int)id);
            Console.WriteLine(id+"执行完毕,离开！");
            _sem.Release();
        }

        public static async void Test() {
            Console.WriteLine("Before calling GetName, Thread Id: {0}\r\n", Thread.CurrentThread.ManagedThreadId);
            var name = await GetName();
            Console.WriteLine("End calling GetName.\r\n");
            Console.WriteLine("Get result from GetName: {0}", name);
        }
        public static async Task<string> GetName() {
            // 这里还是主线程
            Console.WriteLine("Before calling Task.Run, current thread Id is: {0}", Thread.CurrentThread.ManagedThreadId);
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("'GetName' Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);
                return "Jesse";
            });            
        }

        public static void EnglishGreeting(string name) {
            Console.WriteLine("Morning,{0}",name);
        }

        public static void ChineseGreeting(string name) {
            Console.WriteLine("早上好,{0}",name);
        }                
    }    
}

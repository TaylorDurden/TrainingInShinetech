using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace AsyncMultiThreadDemo
{
    internal class Program
    {
        //异步调用执行完成同步信号
        private static readonly AutoResetEvent AutoEvent = new AutoResetEvent(false);

        public delegate int Deleg(int a, int b);

        private static int WriteSum(int a, int b)
        {
            Console.WriteLine("执行WriteSum的线程ID为：{0},sum ={1}", Thread.CurrentThread.ManagedThreadId, a + b);
            return a + b;
        }
        
        private static void SumDone(IAsyncResult async)
        {
            Thread.Sleep(1000);

            //async中包装了异步方法执行的结果
            //从操作结果async中还原委托
            var proc = ((AsyncResult)async).AsyncDelegate as Deleg;
            //获取异步方法的执行结果
            //var sum = proc?.EndInvoke(async);

            //Console.WriteLine("执行SumDone的线程ID为：{0}，Sum = {1}",
            //    Thread.CurrentThread.ManagedThreadId, proc?.EndInvoke(async));

            if (proc != null)
            {
                var sum = proc.EndInvoke(async);

                Console.WriteLine("执行SumDone的线程ID为：{0}，Sum = {1}",
                    Thread.CurrentThread.ManagedThreadId, sum);
            }

            //使用AsnycState属性获取主线程中传入的同步信号
            //释放同步信号表示异步调用已完成
            ((AutoResetEvent)async.AsyncState).Set();


        }

        private static void Main(string[] args)
        {
            var deleg=new Deleg(WriteSum);

            //采用异步方式调用委托
            //指定SumDone为异步操作完成后的回调函数
            //指定AutoEvent为object参数，用于同步回调函数与主线程间操作
            deleg.BeginInvoke(101, 10, SumDone, AutoEvent);
            Console.WriteLine("主线程ID号为：{0}，异步操作已开始执行···",Thread.CurrentThread.ManagedThreadId);

            AutoEvent.WaitOne();
            Console.WriteLine("异步操作已完成！");

            Console.ReadKey();
        }
    }
}

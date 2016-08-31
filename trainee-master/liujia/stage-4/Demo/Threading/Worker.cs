using System;

namespace Threading
{
    public class Worker
    {
        // Volatile这个关键字用来提醒编译器这个数据成员将被多线程访问
        private volatile bool _shouldStop;

        public void RequestStop()
        {
            _shouldStop = true;
        }

        //在线程启动时此方法将被调用
        public void DoWork()
        {
            while (!_shouldStop)// 如果线程正在运行则打印信息
            {
                Console.WriteLine("worker thread:working...");
            }
            Console.WriteLine("worker thread:terminating gracefully.");
        }
    }
}
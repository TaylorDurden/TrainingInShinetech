using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var task = Call();
            Console.WriteLine("测试");
            Console.WriteLine(task.Result);

            //var reader = new FileReader(100);
            //const string path = "D:\\projectDemo\\qujiangbo\\traineeCurrent\\trainee\\qujiangbo\\readme.md";

            ////Console.WriteLine("Begin to redad file...");
            ////reader.SynReadFile(path);
            //reader.AsynReadFile(path);

            ////Console.WriteLine("Begin to do something.");
            //CountEightMultiple();

            Console.ReadKey();
        }

        public static void CountEightMultiple()
        {
            Thread.Sleep(1000);
            for (var i = 0; i < 100; i++)
            {
                if (i % 8 == 0)
                {
                    Console.WriteLine("8的倍数：{0}", i);
                }
            }
        }
        private static async Task<string> Call()
        {
            var result = await GreetingAsync("world");
            return result;
        }
        public static Task<string> GreetingAsync(string name)
        {
            return Task.Run(() => Greeting(name));
        }
        public static string Greeting(string name)
        {
            Thread.Sleep(3000);

            return $"Hello, {name}";
            //return string.Format("Hello, {0}", name);

        }
    }

    public class FileReader
    {
        private byte[] Buffer { get; set; }
        public int BufferSize { get; set; }
        public FileReader(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer=new byte[BufferSize];
        }

        
        public void SynReadFile(string path)
        {
            Console.WriteLine("--------------synchronous read file begin");
            using (var fs = new FileStream(path, FileMode.Open))
            {
                fs.Read(Buffer, 0, BufferSize);
                var output = System.Text.Encoding.UTF8.GetString(Buffer);
                Console.WriteLine("读取的文件信息：{0}", output);
            }

            Console.WriteLine("--------------synchronous read file end");
        }

        public void AsynReadFile(string path)
        {
            if (File.Exists(path))
            {
                var fs = new FileStream(path, FileMode.Open);
                fs.BeginRead(Buffer, 0, BufferSize, AsyncReadCallback, fs);
            }
            else
            {
                Console.WriteLine("--------------the file does not exist");
            }

        }

        private void AsyncReadCallback(IAsyncResult iAsync)
        {
            var stream = iAsync.AsyncState as FileStream;
            if (stream == null) return;
            Thread.Sleep(1000);
            stream.EndRead(iAsync);
            stream.Close();

            var output = System.Text.Encoding.UTF8.GetString(Buffer);
            Console.WriteLine("读取的文件信息：{0}", output);
        }
    }
}

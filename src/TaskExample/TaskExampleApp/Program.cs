using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //SequentialTask();
            PrintTaskStatus();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        static void PrintTaskStatus()
        {
            var t = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"for {i}");
                    Task.Delay(1000).Wait();
                }
            });
            while (true)
            {
                Console.WriteLine($"task.Status:{ t.Status}");
                Thread.Sleep(1000);
            }
        }
        static void SequentialTask()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            //t1先串行
            var t1 = Task.Factory.StartNew(() =>
            {
                stack.Push(1);
                stack.Push(2);
            });

            //t2,t3并行执行
            var t2 = t1.ContinueWith(t =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t2 result={0},Thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
            });

            //t2,t3并行执行
            var t3 = t1.ContinueWith(t =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t3 result={0},Thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
            });

            //等待t2和t3执行完
            Task.WaitAll(t2, t3);

            //t7串行执行
            var t4 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("当前集合元素个数：{0},Thread id {1}", stack.Count, Thread.CurrentThread.ManagedThreadId);
            });
            t4.Wait();
        }
    }
}

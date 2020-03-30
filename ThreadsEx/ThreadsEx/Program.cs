using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
//using System.Threading;

namespace ThreadsEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Invoking 'UseThreads' method");
            UseThreads();

            Console.WriteLine("Invoking 'UseThreadPool' method");
            UseThreadPool();

            //Thread.Sleep(5000);


            Console.WriteLine("Invoking 'UseParallelLinq' method");
            UseParallelLinq();


        }

        private static void UseParallelLinq()
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Doing work from parallel linq...");

            var numbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i);
            }

            Console.WriteLine("Calling AsParallel without DegreesOfParallelism");
            foreach (var number  in numbers.AsParallel())
            {
                Console.WriteLine(number);                
            }

            Console.WriteLine("Calling AsParallel with DegreesOfParallelism");
            foreach (var number in numbers.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount))
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("Calling AsParallel with DegreesOfParallelism And AsOrdered");
            foreach (var number in numbers.AsParallel().AsOrdered().WithDegreeOfParallelism(Environment.ProcessorCount))
            {
                Console.WriteLine(number);
            }
        }

        private static void UseThreadPool()
        {
            ThreadPool.QueueUserWorkItem(DoWorkThreadPool);

        }

        private static void DoWorkThreadPool(object state)
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine($"Thread pool Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
            decimal num = 0;
            for (int i = 0; i < 4; i++)
            {
                num = i;
            }
            Console.WriteLine($"Num: {num}");
        }

        private static void UseThreads()
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
            var thread = new Thread(DoWork);


            Console.WriteLine($"Thread state before start: {thread.ThreadState}");

            // start the thread
            thread.Start();
            Console.WriteLine($"Thread state after start: {thread.ThreadState}");

            var parameterizedThread = new Thread(DoWorkWithParameter);
            Console.WriteLine($"Parameterized Thread state before start: {parameterizedThread.ThreadState}");
            parameterizedThread.Start(5);

            Console.WriteLine($"Parameterized Thread state after start: {parameterizedThread.ThreadState}");


        }

        private static void DoWorkWithParameter(object paramValue)
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine($"Computation Result: {Convert.ToInt32(paramValue) * Convert.ToInt32(paramValue)}...");
        }

        private static void DoWork()
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Doing work...");
        }
    }
}

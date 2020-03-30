/*
 * Copyright 2016-2019 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: khannan
 * Date: 2019-3-17
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Week10Multithreading
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		private static void Main(string[] args)
		{
			Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
			Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");

			Console.WriteLine("Invoking 'UseThreads' method");
			UseThreads();

			Console.WriteLine("Invoking 'UseThreadPool' method");
			UseThreadPool();

			Console.WriteLine("Invoking 'UseParallelLinq' method");
			UseParallelLinq();

			Console.WriteLine("Invoking 'UseTaskParallelLibrary' method");
			UseTaskParallelLibrary();

			Console.WriteLine("Program complete, press any key to exit...");
			Console.ReadKey();
		}

		private static void UseThreads()
		{
			Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");

			// declare and initialize our new thread
			// with the task of invoking the DoWork method
			var thread = new Thread(DoWork);

			Console.WriteLine($"Thread state before start: {thread.ThreadState}");

			// start the thread
			thread.Start();

			Console.WriteLine($"Thread state after start: {thread.ThreadState}");

			// declare and initialize our new thread
			// with the task of invoking the DoWorkWithParameter method
			var parameterizedThread = new Thread(DoWorkWithParameter);

			Console.WriteLine($"Parameterized Thread state before start: {parameterizedThread.ThreadState}");

			// when starting the thread, we want to supply the parameter to the DoWorkWithParameter method
			parameterizedThread.Start("this is the parameter from the use threads method");

			Console.WriteLine($"Parameterized Thread state after start: {parameterizedThread.ThreadState}");

			// abort the threads we started
			//thread.Abort(); // do not do this
			//parameterizedThread.Abort(); // do not do this
		}

		private static void DoWork()
		{
			Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
			Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
			Console.WriteLine("Doing work...");
		}

		private static void DoWorkWithParameter(object state)
		{
			Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
			Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
			Console.WriteLine($"Doing work with parameter: {state}...");
		}

		private static void UseThreadPool()
		{
			ThreadPool.GetAvailableThreads(out var workerThreads, out var completionPortThreads);

			Console.WriteLine($"Worker threads {workerThreads}");
			Console.WriteLine($"Completion port threads {completionPortThreads}");

			// queue an item to be completed on our thread pool
			// after we have queued an item onto our thread pool
			// the task is set aside within the thread pool
			// and when a thread (any thread in the pool) becomes available
			// the work item that was queued to be completed
			// will allocate that thread and the work will be performed
			// in the background
			ThreadPool.QueueUserWorkItem(DoWorkThreadPool);

			// queue an item with a given parameter
			ThreadPool.QueueUserWorkItem(DoWorkThreadPool, "state content");
		}

		private static void DoWorkThreadPool(object state)
		{
			Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
			Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
			Console.WriteLine("Doing work from thread pool...");
			Console.WriteLine($"State: {state}");
		}

		private static void UseParallelLinq()
		{
			Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
			Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
			Console.WriteLine("Doing work from parallel linq...");

			// generate a list of numbers
			var numbers = new List<int>();

			var random = new Random();

			for (var i = 0; i < 3; i++)
			{
				// generate a thousand numbers between 1 and 500
				numbers.Add(random.Next(1, 500));
			}

			// invoking as parallel, allows us to query and process a result set or collection of items
			// in parallel, meaning that the items to be processed are split up across multiple threads

			// read the numbers collection in parallel and process the items non-sequentially

			Console.WriteLine("Calling AsParallel without DegreesOfParallelism");
			foreach (var number in numbers.AsParallel())
			{
				Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
				Console.WriteLine(number % 2 == 0 ? "number is even" : "number is odd");
			}

			Console.WriteLine("Calling AsParallel with DegreesOfParallelism");
			foreach (var number in numbers.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount))
			{
				Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
				Console.WriteLine(number % 2 == 0 ? "number is even" : "number is odd");
			}
		}

		private static void UseTaskParallelLibrary()
		{
			// invoke one or more operations or actions or methods, etc. in parallel, i.e. in the background
			// or on a background thread
			Parallel.Invoke(() =>
			{
				DoWork();
			}, () =>
			{
				DoWorkWithParameter("state content");
			}, () =>
			{
				Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");
				Console.WriteLine($"Managed thread id: {Thread.CurrentThread.ManagedThreadId}");
				Console.WriteLine("Doing work from UseTaskParallelLibraryMethod...");

				for (var i = 0; i < 10; i++)
				{
					Console.WriteLine(i);
				}
			});
		}
	}
}

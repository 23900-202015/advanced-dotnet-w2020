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
 * User: Nityan Khanna
 * Date: 2019-2-16
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Week7TaskBasedAsyncPattern
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The client.
		/// </summary>
		private static readonly HttpClient client = new HttpClient();

		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns>Returns a task.</returns>
		private static async Task Main(string[] args)
		{
			var tasks = TaskYieldAsync("http://mohawkcollege.ca", 
								"http://canada.ca", 
								"http://google.ca", 
								"http://example.com", 
								"http://microsoft.com", 
								"http://apple.com",
								"http://amazon.com");

			Console.WriteLine("after the task yield has been invoked, but before await");

			var resultTasks = await tasks;

			foreach (var resultTask in resultTasks)
			{
				Console.WriteLine(await resultTask);
			}

			Console.WriteLine("after await");

			Console.ReadKey();
		}

		/// <summary>
		/// Demonstrates a task being yielded, that being, returning control to the caller and continuing the task in a separate managed context.
		/// </summary>
		/// <param name="addresses">The addresses.</param>
		/// <returns>Returns a list of tasks representing web result tasks.</returns>
		private static async Task<IEnumerable<Task<WebResult>>> TaskYieldAsync(params string[] addresses)
		{
			var tasks = new List<Task<WebResult>>();

			tasks.AddRange(addresses.Select(c => GetWebsiteAsync(c, new CancellationTokenSource(3000))));

			Console.WriteLine("before yield");

			// yield control of the method to the caller to continue the async processing
			await Task.Yield();

			Console.WriteLine("after yield");

			Console.WriteLine("before when any");

			// when any of the tasks are complete (even if that is only 1 task of many)
			// return the result of the task to the caller
			// and continue asynchronous processing
			await Task.WhenAny(tasks);

			Console.WriteLine("after when any");

			return tasks;
		}

		/// <summary>
		/// Gets a website as an asynchronous operation.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="cancellationTokenSource">The cancellation token source.</param>
		/// <returns>Returns a web result.</returns>
		private static async Task<WebResult> GetWebsiteAsync(string address, CancellationTokenSource cancellationTokenSource)
		{
			// contact the website, starting the task on a background thread
			var getTask = client.GetAsync(address, cancellationTokenSource.Token);

			Console.WriteLine($"Retrieving website... {address} on thread: {Thread.CurrentThread.ManagedThreadId}");

			// await the task to complete
			var response = await getTask;

			Console.WriteLine($"Website {address} retrieved successfully");

			// start the process of reading the content of the retrieved website
			// on a background thread
			var contentTask = response.Content.ReadAsStringAsync();

			Console.WriteLine($"Reading content from website... {address} on thread: {Thread.CurrentThread.ManagedThreadId}");

			return new WebResult(address)
			{
				Content = await contentTask
			};
		}
	}
}

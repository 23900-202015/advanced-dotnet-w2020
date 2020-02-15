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
 * Date: 2019-2-14
 */
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Week6AsyncAwait
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
		private static async Task Main(string[] args)
		{
			// starting our async tasks
			var exampleTask = GetExampleDotComAsync();
			var mohawkCollegeTask = GetMohawkCollegeDotCaAsync();

			// performing synchronous work
			Console.WriteLine("do other work here");

			// await the first async task to completion
			await exampleTask;

			// do more synchronous work
			Console.WriteLine("do work after the example task completion but before the mohawk task completion");

			// await the second async task to completion
			var result = await mohawkCollegeTask;

			// print the result of the second async task
			Console.WriteLine(result);

			Console.Clear();

			Console.WriteLine("this is before the forced task");

			// using the await keyword directly on a method invocation
			// will force the task to be completed at the time of invocation
			// no 'asynchronous-ness' will happen here
			var forcedResult = await ForceTaskCompletionAsync();

			// print after the forced async task is complete
			Console.WriteLine("this is after the forced task");

			Console.ReadKey();
		}

		// starts an asynchronous task, to contact the example.com website
		// however, we are not explicitly returning anything from this method
		private static async Task GetExampleDotComAsync()
		{
			Console.WriteLine("IO (Example.com) task starting");
			// this is IO bound code, because we are accessing a network resource
			var result = await client.GetStringAsync("http://example.com");

			Console.WriteLine("IO task about to be printed");

			// print the result of the task
			Console.WriteLine(result);
		}

		// starts an asynchronous task, to contact the mohawk college website		
		// and returns the result as a string
		private static async Task<string> GetMohawkCollegeDotCaAsync()
		{
			Console.WriteLine("IO (Mohawk College) task starting");

			// this is IO bound code, because we are accessing a network resource
			var result = await client.GetStringAsync("http://mohawkcollege.ca");

			Console.WriteLine("IO task about to be returned");

			return result;
		}

		private static async Task<string> ForceTaskCompletionAsync()
		{
			Console.WriteLine("forced task starting");

			var result = await client.GetStringAsync("http://microsoft.com");

			Console.WriteLine("forced task returning");

			return result;
		}
	}
}

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
using System.Threading.Tasks;

namespace Week6IoCode
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
			// starts the task
			Task task = DoIoStuffAsync();

			Console.WriteLine("this is synchronous");
			Console.WriteLine("this is more synchronous");
			Console.WriteLine("this happens before the await call");

			// await the task to completion
			await task;

			Console.WriteLine("I am after the await task");

			Console.ReadKey();
		}

		private static async Task DoIoStuffAsync()
		{
			Console.WriteLine("IO task starting");
			// this is IO bound code, because we are accessing a network resource
			var result = await client.GetStringAsync("http://example.com");

			Console.WriteLine("IO task about to be printed");

			// print the result of the task
			Console.WriteLine(result);
		}
	}
}

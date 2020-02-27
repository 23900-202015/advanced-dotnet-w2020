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
using System.Reflection;
using System.Threading.Tasks;

namespace Week6TaskWithExceptionAsyncVoid
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
            try {
                await GetExampleDotComAsync();
            }
            catch (Exception ex) {
                Console.WriteLine($"Main Catch: {ex.Message}");
            }
            Console.WriteLine("Back To main, now press any key to quit!");
            Console.ReadKey();

        }

		/// <summary>
		/// Retrieves the contents of the website http://example.com.
		/// </summary>
		/// <returns>Returns a task representing the contents of http://example.com.</returns>
		private static async Task GetExampleDotComAsync()
		{
            var siteList = new List<string> { "yahoo", "google", "msn", "This is not a site", "reddit", "stackoverflow", "wired"};
            int sumLength = 0;

            foreach (string site in siteList)
            {
                //try
                //{
                    var task = client.GetStringAsync($"http://{site}.com");

                    await task;
                    Console.WriteLine($"{site} content length is {task.Result.Length}");

                    sumLength += task.Result.Length;
                //}
                //catch
                //{
                    //Console.WriteLine($"There was an error with {site}");
                //}
            }

            //List<Task<string>> taskList = (from site in siteList select client.GetStringAsync($"http://{site}.com")).ToList();
            //while (taskList.Any()) {
            //    var firstToFinish = await Task.WhenAny(taskList);
            //    Console.WriteLine($" content length is {firstToFinish.Result.Length}");
            //    sumLength += firstToFinish.Result.Length;
            //    taskList.Remove(firstToFinish);

            //}

            Console.WriteLine($"Total length is: {sumLength}");
            //return task.Result;
        }

        /// <summary>
        /// Retrieves the contents of the website http://example.com with exception handling.
        /// </summary>
        /// <returns>Returns a task representing the contents of http://example.com.</returns>
        private static async Task<string> GetExampleDotComWithExceptionHandlingAsync()
		{
			Task<string> task = null;

			//try
			//{
			//	task = GetExampleDotComAsync();

			//	Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} task running...");

			//	await task;

			//	Console.WriteLine($"{MethodBase.GetCurrentMethod().Name} task complete...");
			//}
			//catch (Exception e)
			//{
			//	Console.WriteLine($"Exception occurred in {MethodBase.GetCurrentMethod().Name}, {e}");
			//}

			return task?.Result;
		}

		private static async void GetExampleDotComWithExceptionHandlingVoidAsync()
		{
			// TODO: implement
		}
	}
}

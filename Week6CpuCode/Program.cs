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
using System.Threading;
using System.Threading.Tasks;

namespace Week6CpuCode
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	public class Program
	{
		private static Person person = new Person("mary");

		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>'
		// the async keyword marks a block of code
		// which could be a method body, lambda expression, etc
		// as asynchronous
		private static async Task Main(string[] args)
		{
			// starts our CPU bound code to run our task
			Task task = DoCpuStuffAsync();
			Task task2 = DoPersonThingsAsync();


			Console.WriteLine("hello");
			Console.WriteLine("this is a test");
			Console.WriteLine("this code is being run synchronously");

			// print the persons name
			Console.WriteLine(person.Name);

			// the await keyword indicates to the compiler
			// that 'await Task.Delay(1000);'
			// is a suspension point inside this method or block of code
			await Task.Delay(1000);

			// await our task to completion
			await task;

			// await our task to completion
			await task2;

			Console.ReadKey();
		}

		private static async Task DoCpuStuffAsync()
		{
			await Task.Factory.StartNew(() =>
			{
				// CPU bound code
				// this code has no dependency on
				// a network
				// or database
				// or external resource
				// this code only depends on the CPU
				for (int i = 0; i < 10; i++)
				{
					Console.WriteLine(i);
				}
			});
		}

		private static async Task DoPersonThingsAsync()
		{
			await Task.Delay(5000);
			await Task.Factory.StartNew(() =>
			{
				Console.WriteLine("updated person name to john");
				person.Name = "John";
			});
		}
	}

	public class Person
	{
		public Person()
		{
			
		}

		public Person(string name)
		{
			this.Name = name;
		}

		public string Name { get; set; }
	}
}

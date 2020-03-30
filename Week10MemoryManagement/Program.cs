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
using System.IO;

namespace Week10MemoryManagement
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
			Console.WriteLine($"Total memory before processing file:{GC.GetTotalMemory(false):N0}");

			Console.WriteLine("Generating lots of objects...");
			// generate lots of objects
			//GenerateObjects();

			// write to our file
			File.WriteAllText(@"C:\sample.txt", "this is some content of our file");

			// open the sample.txt file on our computer
			// this will create a file stream instance
			// and we want to pass our filestream instance to our disposable object instance
			// use a using statement to implement the auto-disposal
			// of resources within an application context
			// as soon as the code inside the using statement is completed execution
			// the object instance within the brackets, in our case 'streamReader'
			// is disposed of and all the resources it was using are cleaned up via disposal
			// create a new instance of our stream reader
			// provide the filestream we want to read
			StreamReader streamReader = null;

			using (var fileStream = File.Open(@"C:\sample.txt", FileMode.Open))
			using (var myDisposableObject = new MyDisposableObject("hello", fileStream))
			using (streamReader = new StreamReader(myDisposableObject.FileStream))
			{
				// read the contents of the stream to the end of the stream, and write them to the screen
				Console.WriteLine($"File contents:{streamReader.ReadToEnd()}");

				// at the end of our dispose method on our disposable object instance
				// which in-turn will remove dispose of our file stream instance

			} // when code runtime reaches this bracket, the object in the using statement will be disposed

			// print the amount of memory already allocated

			Console.WriteLine($"Total memory while processing file:{GC.GetTotalMemory(false):N0}");

			// force garbage collection
			GC.Collect();

			Console.WriteLine($"Total memory after disposal of file:{GC.GetTotalMemory(false):N0}");

			// attempt to read the stream again
			// an exception will be thrown here
			// because we are attempting to access an instance of our object
			// that has already been disposed of, meaning that all the resources
			// it was using to 'operate' or 'function' no longer exist in the context
			// of our application
			// therefore we are unable to access and functions this object provides
			// that use the underlying resources
			streamReader.ReadToEnd();

			Console.ReadKey();
		}

		// generate a lot of objects and re-assign them to the previous instance
		// this is very inefficient, and should not be done
		private static void GenerateObjects()
		{
			object o;

			for (var i = 0; i < 10; i++)
			{
				o = new object();

				// on every other iteration, we want to call the garbage collector
				if (i % 2 == 0)
				{
					// the collect method, allows us to manually invoke the garbage collector
					// and force the collection process of cleaning up objects
					// that are no longer in use
					GC.Collect();
				}
			}
		}
	}
}

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
using System.IO;
using System.Threading.Tasks;

namespace Week7AsyncProgrammingModel
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
		/// <returns>Returns a task.</returns>
		private static async Task Main(string[] args)
		{
			// get the path of the app data folder
			var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

			// create an DirectoryInfo object instance which contains information about the directory
			var directoryInfo = new DirectoryInfo(path);

			// retrieve all the files ending with file extension .txt recursively
			var files = directoryInfo.GetFiles("*.txt", SearchOption.AllDirectories);

			Console.WriteLine($"Found {files.Length} files");

			var buffer = new byte[1024];

			foreach (var fileInfo in files)
			{
				await Task.Yield();

				var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);

				stream.BeginRead(buffer, 0, buffer.Length, HandleRead, stream);
			}

			Console.ReadKey();
		}

		private static void HandleRead(IAsyncResult result)
		{
			var fileStream = (FileStream)result.AsyncState;
			var bytesRead = fileStream.EndRead(result);

			Console.WriteLine($"Read {bytesRead} bytes from file: {fileStream.Name}");

			//fileStream.BeginRead(buffer, 0, buffer.Length, HandleRead, fileStream);
		}
	}
}

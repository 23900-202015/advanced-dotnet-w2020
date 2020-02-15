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
 * Date: 2019-1-31
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Week5SerializationCore;

namespace Week5BinarySerialization
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
			// declare and initialize our BinaryFormatter class
			// this class is used for binary serialization
			var formatter = new BinaryFormatter();

			// declare and initialize our memory stream
			// the purpose of this memory stream
			// is to hold the serialized content in memory
			var stream = new MemoryStream();

			// create the object to be serialized
			var person = new Person
			{
				Name = "Mary Smith"
			};

			// serialize the object
			// 'person' is our object we want to serialize
			// 'stream' is the instance of the stream to write the
			// serialized version of the object to
			// formatter indicates the type of serialization
			// we are performing on the object
			// 'serialize' is the action to perform
			formatter.Serialize(stream, person);

			// convert the content of the serialized stream
			// to a byte array
			byte[] serializedContent = stream.ToArray();

			// print the contents of the byte array
			foreach (var content in serializedContent)
			{
				Console.WriteLine(content);
			}

			Console.WriteLine("write our serialized content to a file called 'output.bin'");

			// write our serialized content to a file called 'output.bin'
			File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}\\output.bin", serializedContent);

			Console.WriteLine("read our serialized content from a file called 'output.bin'");
			var bytes = File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}\\output.bin");

			var deserializedPerson = (Person)formatter.Deserialize(new MemoryStream(bytes));

			Console.WriteLine(deserializedPerson.Name);

			Console.ReadKey();
		}
	}
}

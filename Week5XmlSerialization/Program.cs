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
using System.Xml.Serialization;
using Week5SerializationCore;

namespace Week5XmlSerialization
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
			// declare and initialize our xml serializer
			// supply type of person to indicate what type of object
			// is to be serialized
			var serializer = new XmlSerializer(typeof(Person));

			var memoryStream = new MemoryStream();

			var person = new Person
			{
				Name = "Mary Smith",
				DateOfBirth = DateTimeOffset.Now,
				Id = Guid.NewGuid()
			};

			serializer.Serialize(memoryStream, person);

			var serializedContent = memoryStream.ToArray();

			Console.WriteLine("write our serialized content to a file called 'output.xml'");

			// write our serialized content to a file called 'output.xml'
			File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}\\output.xml", serializedContent);

			Console.WriteLine("read our serialized content from a file called 'output.xml'");
			var bytes = File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}\\output.xml");

			var deserializedPerson = (Person)serializer.Deserialize(new MemoryStream(bytes));

			Console.WriteLine(deserializedPerson.Name);

			Console.ReadKey();
		}
	}
}

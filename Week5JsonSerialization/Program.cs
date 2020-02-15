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
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Week5SerializationCore;

namespace Week5JsonSerialization
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
			// declare and initialize our json serializer
			// supply type of person to indicate what type of object
			// is to be serialized
			var serializer = new JsonSerializer();

			var stringWriter = new StringWriter();

			var person = new Person
			{
				Name = "Mary Smith",
				DateOfBirth = DateTimeOffset.Now,
				Id = Guid.NewGuid()
			};

			serializer.Serialize(stringWriter, person);

			var serializedContent = Encoding.UTF8.GetBytes(stringWriter.ToString());

			Console.WriteLine("write our serialized content to a file called 'output.json'");

			// write our serialized content to a file called 'output.json'
			File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}\\output.json", serializedContent);

			Console.WriteLine("read our serialized content from a file called 'output.json'");
			var bytes = File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}\\output.json");

			var jsonReader = new JsonTextReader(new StringReader(Encoding.UTF8.GetString(bytes)));

			var deserializedPerson = (Person)serializer.Deserialize(jsonReader, typeof(Person));

			Console.WriteLine(deserializedPerson.Name);

			Console.ReadKey();
		}
	}
}

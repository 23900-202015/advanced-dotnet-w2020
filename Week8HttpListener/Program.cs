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
 * Date: 2019-2-23
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace Week8HttpListener
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The dictionary to hold the content of POST requests.
		/// </summary>
		private static Dictionary<Guid, Person> personStore = new Dictionary<Guid, Person>();

		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		private static void Main(string[] args)
		{
			// declare and initialize our HTTP listener
			var listener = new HttpListener();

			// add our prefixes
			listener.Prefixes.Add("http://127.0.0.1:21000/demo/");
			listener.Prefixes.Add("http://127.0.0.1:21000/test/");
			listener.Prefixes.Add("http://127.0.0.1:21000/dev/");
			listener.Prefixes.Add("http://127.0.0.1:21000/hello/");
			listener.Prefixes.Add("http://127.0.0.1:21000/xml/");
			listener.Prefixes.Add("http://127.0.0.1:21000/post/");
			listener.Prefixes.Add("http://127.0.0.1:21000/getperson/");

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Listener starting");

			// start our HTTP listener
			listener.Start();

			foreach (var prefix in listener.Prefixes)
			{
				Console.WriteLine($"Reading to accept requests on: {prefix}");
			}

			Console.ResetColor();

			// indicate to the HTTP listener that we want to start accepting requests asynchronously
			// the method used to handle the requests is the first parameter 'HandleRequest'
			// the second parameter passes the listener instance to continue the asynchronous pipeline
			listener.BeginGetContext(HandleRequest, listener);

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		/// <summary>
		/// Handles the request.
		/// </summary>
		/// <param name="result">The result.</param>
		private static async void HandleRequest(IAsyncResult result)
		{
			// cast our async state, back to the listener instance
			var listener = (HttpListener)result.AsyncState;

			// indicate to the asynchronous pipeline that this request is being processed
			// and allow other requests to start
			var context = listener.EndGetContext(result);

			Console.WriteLine($"Received request from: {context.Request.RemoteEndPoint}");

			byte[] response;

			// set the content type to indicate to the client what mime type the results are in
			context.Response.ContentType = "text/plain;charset=UTF-8";

			var serializer = new XmlSerializer(typeof(XmlResult));
			var memoryStream = new MemoryStream();

			// handle the request differently based on the path accessed
			switch (context.Request.Url.LocalPath)
			{
				case "/dev":
					response = SerializeResponse("this is the dev endpoint");
					break;
				case "/hello":
					response = SerializeResponse("this is the hello endpoint");
					break;
				case "/test":
					response = SerializeResponse("this is the test endpoint");
					break;
				case "/demo":
					response = SerializeResponse("this is the demo endpoint");
					break;
				case "/xml":
					var xmlResult = new XmlResult("this is content from the XML endpoint");

					serializer.Serialize(memoryStream, xmlResult);

					response = memoryStream.ToArray();
					context.Response.ContentType = "application/xml";
					break;
				case "/getperson":
					context.Response.ContentType = "application/xml";
					serializer = new XmlSerializer(typeof(Person));

					var person = personStore[Guid.Parse(context.Request.QueryString.GetValues("id").FirstOrDefault())];


					serializer.Serialize(memoryStream, person);

					response = memoryStream.ToArray();
					break;
				case "/post":
					context.Response.ContentType = "application/xml";
					serializer = new XmlSerializer(typeof(Person));

					serializer.Serialize(memoryStream, HandlePost(context));

					response = memoryStream.ToArray();
					break;
				default:
					response = SerializeResponse("not found");
					break;
			}

			// asynchronously start to write the response to the client
			var writeResponseTask = context.Response.OutputStream.WriteAsync(response, 0, response.Length);

			// set the HTTP status code
			context.Response.StatusCode = 200;

			// await the completion of the task
			await writeResponseTask;

			// close response context
			context.Response.Close();

			// start listening again
			listener.BeginGetContext(HandleRequest, listener);
		}

		/// <summary>
		/// Serializes the response.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <returns>Returns a byte array representing the response.</returns>
		private static byte[] SerializeResponse(string content)
		{
			Console.WriteLine($"Sending response to client: {content}");

			return Encoding.UTF8.GetBytes(content);
		}

		private static Person HandlePost(HttpListenerContext context)
		{
			// use the XML serializer to deserialize and process our POST request
			var serializer = new XmlSerializer(typeof(Person));
			var memoryStream = new MemoryStream();

			// deserialize the request input stream to a Person instance
			var person = (Person)serializer.Deserialize(context.Request.InputStream);

			// add the deserialized person to our person store
			personStore.Add(person.Id, person);

			// return the added person
			return person;
		}
	}

	[XmlRoot]
	[XmlType]
	public class Person
	{
		public Person()
		{
			
		}

		[XmlElement]
		public Guid Id { get; set; }

		[XmlElement]
		public string Name { get; set; }

	}

	[XmlRoot("result", Namespace = "http://example.com/demo")]
	[XmlType("result", Namespace = "http://example.com/demo")]
	public class XmlResult
	{
		public XmlResult() : this(Guid.NewGuid())
		{
		}

		public XmlResult(Guid id)
		{
			this.Id = id;
		}

		public XmlResult(string content) : this(Guid.NewGuid(), content)
		{
			
		}

		public XmlResult(Guid id, string content) : this(id)
		{
			this.Content = content;
		}

		

		[XmlElement]
		public Guid Id { get; set; }

		[XmlElement]
		public string Content { get; set; }
	}
}

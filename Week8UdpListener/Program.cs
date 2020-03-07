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
 * Date: 2019-3-7
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Week8UdpListener
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
		private static async Task Main(string[] args)
		{
			var udpListener = new UdpClient(new IPEndPoint(IPAddress.Any, 22001));

			udpListener.BeginReceive(async (o) => await ProcessUdpRequestAsync(o), udpListener);

			for (var i = 0; i < 5; i++)
			{
				var client = new UdpClient();

				client.Connect(new IPEndPoint(IPAddress.Loopback, 22001));

				var content = Encoding.ASCII.GetBytes("some content");

				await client.SendAsync(content, content.Length);

				client.Close();
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Process UDP request as an asynchronous operation.
		/// </summary>
		/// <param name="asyncResult">The asynchronous result.</param>
		/// <returns>Returns a task.</returns>
		private static async Task ProcessUdpRequestAsync(IAsyncResult asyncResult)
		{
			var listener = (UdpClient)asyncResult.AsyncState;
			var endpoint = new IPEndPoint(IPAddress.Any, 0);
			var context = listener.EndReceive(asyncResult, ref endpoint);

			listener.BeginReceive(async (o) => await ProcessUdpRequestAsync(o), listener);

			Console.WriteLine($"Data received from client: {Encoding.ASCII.GetString(context)}");

			var content = Encoding.ASCII.GetBytes("this is a response from the UDP server");

			await listener.SendAsync(content, content.Length, endpoint);
		}
	}
}

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

namespace Week8TcpListener
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
			var tcpListener = new TcpListener(IPAddress.Any, 22000);

			tcpListener.Start();

			tcpListener.BeginAcceptTcpClient(async (o) => await ProcessTcpRequestAsync(o), tcpListener);

			Console.WriteLine("Hello World!");
			Console.ReadKey();
		}

		/// <summary>
		/// Process TCP request as an asynchronous operation.
		/// </summary>
		/// <param name="asyncResult">The asynchronous result.</param>
		/// <returns>Returns a task.</returns>
		private static async Task ProcessTcpRequestAsync(IAsyncResult asyncResult)
		{
			var listener = (TcpListener)asyncResult.AsyncState;
			var context = listener.EndAcceptTcpClient(asyncResult);

			listener.BeginAcceptTcpClient(async (o) => await ProcessTcpRequestAsync(o), listener);

			var stream = context.GetStream();

			var buffer = new byte[1024];
			int position;
			var data = string.Empty;

			while ((position = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)

			{
				data += Encoding.ASCII.GetString(buffer, 0, position);

				Console.WriteLine($"Data received from client: {data}");

				var response = Encoding.ASCII.GetBytes("this is a response from the TCP server");

				await stream.WriteAsync(response, 0, response.Length);
			}

			stream.Close();
		}
	}
}

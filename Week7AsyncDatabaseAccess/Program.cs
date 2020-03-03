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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Week7AsyncDatabaseAccess.Data;
using Week7AsyncDatabaseAccess.Data.ViewModel;
using Week7AsyncDatabaseAccess.Services;

namespace Week7AsyncDatabaseAccess
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The configuration.
		/// </summary>
		private static IConfiguration configuration;

		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns>Returns a task.</returns>
		private static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args);

			var task = host.StartAsync();

			string input;

			do
			{
				Console.ResetColor();
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Welcome to the person creator...");
				Console.WriteLine("Please see an action to perform below...");
				Console.WriteLine("1 - Create a person");
				Console.WriteLine("2 - Print a list of all persons");
				Console.WriteLine("3 - Exit");
				Console.ResetColor();

				input = Console.ReadLine();

				switch (input)
				{
					case "1":
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("1 has been entered, will now start the create person workflow...");
						await CreatePersonAsync(host);
						break;
					case "2":
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("2 has been entered, will now start the query person workflow...");
						await QueryPersonAsync(host);
						break;
					case "3":
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("3 has been entered, the program will initiate the stop process...");
						break;
					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Invalid menu entry: {input}, please enter a valid menu entry...");
						break;
				}

				Console.ResetColor();

			} while (input != "3");

			await task;
		}

		/// <summary>
		/// Creates the host builder.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns>Returns the created host.</returns>
		private static IHost CreateHostBuilder(string[] args)
		{
			// create the configuration builder to load our app.config
			var configurationBuilder = new ConfigurationBuilder()
										.SetBasePath(Directory.GetCurrentDirectory())
										.AddXmlFile("app.config");

			// build the configuration
			configuration = configurationBuilder.Build();

			// build the host context
			var host = new HostBuilder()
						.ConfigureHostConfiguration((c) => c.AddConfiguration(configuration))
						.ConfigureServices((context, services) =>
						{
							// add the database context to the list of services
							services.AddDbContext<ApplicationDbContext>((options) =>
							{
								options.UseInMemoryDatabase(configuration.GetValue<string>("databaseName"));
							});

							var serviceProvidersSection = configuration.GetSection("serviceProviders:add");

							// load the service providers from our configuration file
							var types = serviceProvidersSection.GetChildren().Select(c =>
							{
								Console.ResetColor();
								var type = Type.GetType(c.Value);

								if (type == null)
								{
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine($"Could not load type {c.Value}...");
								}
								else
								{
									Console.ForegroundColor = ConsoleColor.Green;
									Console.WriteLine($"Loading type {type}...");
								}

								Console.ResetColor();
								return type;
							});

							// select all the interfaces of each type and add them as transient services
							var addedTypes = types.SelectMany(t => t.GetInterfaces().Select(c =>
							{
								services.AddTransient(c, t);
								return t;
							})).ToArray();

							if (!addedTypes.Any())
							{
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine("Warning, no services loaded...");
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine($"Successfully loaded {addedTypes.Count()} services...");
							}
						});

			// load the environment setting from our configuration file
			var environment = configuration.GetValue<string>("environment");

			switch (environment)
			{
				case "Development":
				case "Staging":
				case "Production":
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(environment), "Invalid value for environment, valid values are: Development, Staging, Production");
			}

			// set the environment
			host.UseEnvironment(environment);

			// build and return the host
			return host.Build();
		}

		/// <summary>
		/// Creates a person asynchronously.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns>Returns a task.</returns>
		private static async Task CreatePersonAsync(IHost host)
		{
			// retrieve our person service
			var personService = host.Services.GetService<IPersonService>();

			Console.WriteLine("Please enter the details of the person to create...");

			Console.WriteLine("First name:");
			var firstName = Console.ReadLine();

			Console.WriteLine("Last name:");
			var lastName = Console.ReadLine();

			// start the task
			var createTask = personService.CreatePersonAsync(firstName, lastName);

			Console.WriteLine("Saving new person to the database...");

			// await the task
			await createTask;
		}

		/// <summary>
		/// Queries for a person asynchronously.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns>Returns a list of persons which match the given predicate.</returns>
		private static async Task<IAsyncEnumerable<PersonViewModel>> QueryPersonAsync(IHost host)
		{
			// retrieve our person service
			var personService = host.Services.GetService<IPersonService>();

			Console.WriteLine("Please enter the name of a person to find the database...");
			var name = Console.ReadLine()?.ToLowerInvariant();

			// start and await the task
			var results = await personService.QueryPersonAsync(c => c.FirstName.ToLowerInvariant().Contains(name) || c.LastName.ToLowerInvariant().Contains(name));

			Console.Write(Environment.NewLine);

			// print each result to the screen
			results.ForEach(Console.WriteLine);

			return results.ToAsyncEnumerable();
		}
	}
}

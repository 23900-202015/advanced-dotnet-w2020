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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Week7AsyncDatabaseAccess.Data;
using Week7AsyncDatabaseAccess.Data.Model;
using Week7AsyncDatabaseAccess.Data.ViewModel;

namespace Week7AsyncDatabaseAccess.Services
{
	/// <summary>
	/// Represents a person service.
	/// </summary>
	/// <seealso cref="Week7AsyncDatabaseAccess.Services.IPersonService" />
	public class PersonService :  IPersonService
	{
		/// <summary>
		/// The context.
		/// </summary>
		private readonly ApplicationDbContext context;

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonService" /> class.
		/// </summary>
		public PersonService(ApplicationDbContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Creates a person asynchronously.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <returns>Returns a task.</returns>
		public async Task<PersonViewModel> CreatePersonAsync(string firstName, string lastName)
		{
			// create our person object to be saved to the database
			var person = new Person(firstName, lastName);

			// start the process of adding our created person instance to the entity context
			// the entity context being, a object or objects to be saved to the database at a later point in time
			var addTask = this.context.Persons.AddAsync(person);

			Console.WriteLine($"adding person {firstName}, {lastName} to entity context...");

			// await our add task to completion
			await addTask;

			// start the process of saving the changes to the database
			var saveTask = this.context.SaveChangesAsync();

			Console.WriteLine("saving changes...");

			// create the response model
			var viewModel = new PersonViewModel(person);

			// await our save task to completion
			await saveTask;

			// return the response model
			return viewModel;
		}

		/// <summary>
		/// Queries for a person asynchronously.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <returns>Returns a list of persons which match the given predicate.</returns>
		public async Task<List<PersonViewModel>> QueryPersonAsync(Expression<Func<Person, bool>> expression)
		{
			// query the database
			var results = this.context.Persons.Where(expression);

			// start the process of processing the results from the database
			// using the ToListAsync method
			var queryTask = results.Select(c => new PersonViewModel(c)).ToListAsync();

			Console.WriteLine("querying persons...");

			// await the task to completion
			return await queryTask;
		}

		public async Task DeletePersonAsync(Guid id)
		{
			// find the record to delete
			var person = this.context.Persons.Find(id);

			if (person != null)
			{
				this.context.Persons.Remove(person);
				await this.context.SaveChangesAsync();
			}
		}

	}
}

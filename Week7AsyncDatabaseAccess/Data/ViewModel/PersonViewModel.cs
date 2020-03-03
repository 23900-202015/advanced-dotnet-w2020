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
using Week7AsyncDatabaseAccess.Data.Model;

namespace Week7AsyncDatabaseAccess.Data.ViewModel
{
	/// <summary>
	/// Represents a person view model.
	/// </summary>
	public class PersonViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonViewModel" /> class.
		/// </summary>
		public PersonViewModel()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonViewModel"/> class.
		/// </summary>
		/// <param name="person">The person.</param>
		public PersonViewModel(Person person)
		{
			this.CreationTime = person.CreationTime;
			this.FirstName = person.FirstName;
			this.LastName = person.LastName;
		}

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>The creation time.</value>
		public DateTimeOffset CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		public string LastName { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents the creation time, first name and last name of the person.
		/// </summary>
		/// <returns>Returns a <see cref="System.String" /> that represents the creation time, first name and last name of the person.</returns>
		public override string ToString()
		{
			return $"Creation Time: {this.CreationTime:yyyy-MM-dd}, Last name:{this.LastName}, First name:{this.FirstName}";
		}
	}
}

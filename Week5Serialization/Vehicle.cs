/*
 * Copyright 2016-2020 Mohawk College of Applied Arts and Technology
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
 * Date: 2020-2-1
 */
using System;

namespace Week5Serialization
{
	/// <summary>
	/// Represents a vehicle.
	/// </summary>
	public abstract class Vehicle
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Vehicle"/> class.
		/// </summary>
		protected Vehicle() : this(Guid.NewGuid())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Vehicle"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		protected Vehicle(Guid id)
		{
			this.Id = id;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Vehicle"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="dateOfManufacture">The date of manufacture.</param>
		protected Vehicle(Guid id, DateTimeOffset? dateOfManufacture) : this(id)
		{
			this.DateOfManufacture = dateOfManufacture;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Vehicle"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="dateOfManufacture">The date of manufacture.</param>
		/// <param name="name">The name.</param>
		protected Vehicle(Guid id, DateTimeOffset? dateOfManufacture, string name) : this(id, dateOfManufacture)
		{
			this.Name = name;
		}

		/// <summary>
		/// Gets or sets the date of manufacture.
		/// </summary>
		/// <value>The date of manufacture.</value>
		public DateTimeOffset? DateOfManufacture { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
	}
}
